using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Octokit;

namespace GitHub.TemplateSelectors
{
    public class TreeItemTemplateSelector : DataTemplateSelector
    {
        public DataTemplate FolderTemplate { get; set; }
        public DataTemplate FileTemplate { get; set; }
        public DataTemplate TopFolderTemplate { get; set; }


        protected override DataTemplate SelectTemplateCore(object item)
        {
            var treeItem = item as TreeItem;

            if (treeItem != null)
            {
                if (treeItem.Path == "..")
                    return TopFolderTemplate;

                if (treeItem.Type == TreeType.Tree)
                    return FolderTemplate;

                if (treeItem.Type == TreeType.Blob)
                    return FileTemplate;
            }

            return base.SelectTemplateCore(item);
        }
    }
}
