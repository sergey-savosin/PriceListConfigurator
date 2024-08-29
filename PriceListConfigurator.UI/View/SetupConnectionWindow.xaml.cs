﻿using PriceListConfigurator.Model;
using PriceListConfigurator.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PriceListConfiguratorUI.View
{
    /// <summary>
    /// Interaction logic for SetupConnectionWindow.xaml
    /// </summary>
    public partial class SetupConnectionWindow : Window
    {
        private int m_OriginalIndex;
        private DataGridRow m_OldRow;
        private Connection m_TargetItem;
        private SetupConnectionViewModel m_ViewModel;

        public SetupConnectionWindow(SetupConnectionViewModel viewModel)
        {
            InitializeComponent();

            m_ViewModel = viewModel;
            DataContext = m_ViewModel;

            Closing += m_ViewModel.OnWindowClosing;
        }

        private static T FindVisualParent<T>(UIElement element) where T : UIElement
        {
            var parent = element;
            while (parent != null)
            {
                var correctlyTyped = parent as T;
                if (correctlyTyped != null)
                {
                    return correctlyTyped;
                }

                parent = VisualTreeHelper.GetParent(parent) as UIElement;
            }
            return null;
        }

        /// <summary>
        /// Updates the grid as a drag progresses
        /// </summary>
        private void OnMainGridCheckDropTarget(object sender, DragEventArgs e)
        {
            var row = FindVisualParent<DataGridRow>(e.OriginalSource as UIElement);

            /* If we are over a row that contains a GroceryItem, set 
             * the drop-line above that row. Otherwise, do nothing. */

            // Set the DragDropEffects 
            if ((row == null) || !(row.Item is Connection))
            {
                e.Effects = DragDropEffects.None;
            }
            else
            {
                var currentIndex = row.GetIndex();

                // Erase old drop-line
                if (m_OldRow != null) m_OldRow.BorderThickness = new Thickness(0);

                // Draw new drop-line
                var direction = (currentIndex - m_OriginalIndex);
                if (direction < 0) row.BorderThickness = new Thickness(0, 2, 0, 0);
                else if (direction > 0) row.BorderThickness = new Thickness(0, 0, 0, 2);

                // Reset old row
                m_OldRow = row;
            }
        }

        /// <summary>
        /// Gets the view model from the data Context and assigns it to a member variable.
        /// </summary>
        private void OnMainGridDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            m_ViewModel = (SetupConnectionViewModel)this.DataContext;
        }

        /// <summary>
        /// Process a row drop on the DataGrid.
        /// </summary>
        private void OnMainGridDrop(object sender, DragEventArgs e)
        {
            e.Effects = DragDropEffects.None;
            e.Handled = true;

            // Verify that this is a valid drop and then store the drop target
            var row = FindVisualParent<DataGridRow>(e.OriginalSource as UIElement);
            if (row != null)
            {
                m_TargetItem = row.Item as Connection;
                if (m_TargetItem != null)
                {
                    e.Effects = DragDropEffects.Move;
                }
            }

            // Erase last drop-line
            if (m_OldRow != null) m_OldRow.BorderThickness = new Thickness(0, 0, 0, 0);
        }

        /// <summary>
        /// Processes a drag in the main grid.
        /// </summary>
        private void OnMainGridMouseMove(object sender, MouseEventArgs e)
        {
            // Exit if shift key and left mouse button aren't pressed
            if (e.LeftButton != MouseButtonState.Pressed) return;
            if (Keyboard.Modifiers != ModifierKeys.Shift) return;

            /* We use the m_MouseDirection value in the 
             * OnMainGridCheckDropTarget() event handler. */

            // Find the row the mouse button was pressed on
            var row = FindVisualParent<DataGridRow>(e.OriginalSource as FrameworkElement);
            if (row != null)
            {
                m_OriginalIndex = row.GetIndex();
            }

            // If the row was already selected, begin drag
            if ((row != null) && row.IsSelected && !row.IsEditing)
            {
                // Get the grocery item represented by the selected row
                var selectedItem = (Connection)row.Item;
                var finalDropEffect = DragDrop.DoDragDrop(row, selectedItem, DragDropEffects.Move);
                if ((finalDropEffect == DragDropEffects.Move) && (m_TargetItem != null))
                {
                    /* A drop was accepted. Determine the index of the item being 
                     * dragged and the drop location. If they are different, then 
                     * move the selectedItem to the new location. */

                    // Move the dragged item to its drop position
                    var oldIndex = m_ViewModel.ConnectionList.IndexOf(selectedItem);
                    var newIndex = m_ViewModel.ConnectionList.IndexOf(m_TargetItem);
                    if (oldIndex != newIndex) m_ViewModel.ConnectionList.Move(oldIndex, newIndex);
                    m_TargetItem = null;
                }
            }
        }

    }
}
