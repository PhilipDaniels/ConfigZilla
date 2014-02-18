using System.Windows.Forms;

namespace ConfigZilla.Encrypter
{
    /// <summary>
    /// Allow tab pages to be dynamically shown and hidden.
    /// </summary>
    public static class TabPageExtensions
    {
        public static bool IsVisible(this TabPage tabPage)
        {
            if (tabPage.Parent == null)
                return false;
            else if (tabPage.Parent.Contains(tabPage))
                return true;
            else
                return false;
        }

        public static void HidePage(this TabPage tabPage)
        {
            if (tabPage.IsVisible())
            {
                TabControl parent = (TabControl)tabPage.Parent;
                parent.TabPages.Remove(tabPage);
            }
        }

        public static void ShowPageInTabControl(this TabPage tabPage, TabControl parent)
        {
            if (!tabPage.IsVisible())
            {
                parent.TabPages.Add(tabPage);
            }
        }
    }
}
