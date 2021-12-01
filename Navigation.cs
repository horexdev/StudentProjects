using System;
using System.Windows;
using System.Windows.Controls;
using StudentProjects;

namespace StudentProjects
{
    public static class Navigation
    {
        private static Frame _frame;

        public static void SetFrame(Frame frame)
        {
            if (_frame != null)
            {
                ErrorsHandler.Throw(ErrorType.FrameAlreadyDefined);

                return;
            }

            _frame = frame;
        }

        public static void Navigate(Page page)
        {
            if (_frame == null)
            {
                ErrorsHandler.Throw(ErrorType.FrameNullReferenceException);

                return;
            }

            _frame.Navigate(page);
        }

        public static void GoBack(object sender, RoutedEventArgs e)
        {
            if (_frame == null)
            {
                ErrorsHandler.Throw(ErrorType.FrameNullReferenceException);

                return;
            }

            _frame.GoBack();
        }
    }
}