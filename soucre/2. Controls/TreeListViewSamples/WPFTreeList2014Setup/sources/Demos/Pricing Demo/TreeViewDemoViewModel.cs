using System;
using Seekford.Controls.WPFTreeListView;

namespace PricingWPFDemo
{
    class TreeViewDemoViewModel:ViewModelBase,ITreeListViewFilter
    {

        public TreeViewDemoViewModel()
        {
            CreateTreeRoot();
            LoadData();
        }

        private void LoadData()
        {            
            //Pretending as if we loaded from database. Creating simple heirarchy of records for a small organization.
            TreeRoot.IsBatchLoading = true;
            var org = new OrganizationTreeModel() { DisplayName = "Seekford Solutions, Inc." };
            var dep = new OrganizationTreeModel() { DisplayName = "Developer Products" };
            var wpf = new OrganizationTreeModel() { DisplayName = ".NET WPF" };
            //var deprecated = new OrganizationTreeModel() { DisplayName = "Deprecated Products" };
            var wpfprod = new OrganizationTreeModel() { DisplayName = "WPF TreeListView 2014" };
            var prod = new ProductTreeModel() { DisplayName = "Single User License", Price = "$199 USD" };


            org.Children.Add(dep);
            //org.Children.Add(deprecated);

            dep.Children.Add(wpf);
            wpf.Children.Add(wpfprod);
            wpfprod.Children.Add(prod);
            TreeRoot.Children.Add(org);
          
            TreeRoot.IsBatchLoading = false;
        }
        
        #region Initialization
        
        private void CreateTreeRoot()
        {
            TreeRoot = new TreeModelRoot();
        }

        #endregion

        #region Properties

        /// <summary>
        /// If we want to programmatically set the selected item. Use TreeRoot for multiple item selections
        /// </summary>

        private TreeNode _selectedNode;
        public TreeNode SelectedNode
        {
            get { return _selectedNode; }
            set
            {
                _selectedNode = value;
                RaisePropertyChanged("SelectedNode");
            }
        }

        private ITreeModelRoot _treeRoot;
        public ITreeModelRoot TreeRoot
        {
            get
            {
                return _treeRoot;
            }
            set 
            {
                _treeRoot = value;
                RaisePropertyChanged("TreeRoot");
            }
        }

        public string NoRowsDefaultMessage
        {
            get
            {
                return "No items";
            }
        }


        #endregion



        #region Filter
        

        public TreeListViewFilterState GetFilterState(ITreeModel treeModel)
        {
            return TreeListViewFilterState.Include;
        }



        #endregion
    }
}
