using System;
using System.Collections.Generic;
using Seekford.Controls.WPFTreeListView;
using System.Windows.Input;

namespace WPFTreeViewDemo
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
            TreeListModel = null;
            TreeRoot = new TreeModelRoot();
                        
            //Pretending as if we loaded from database. Creating simple heirarchy of records for a small organization.
            TreeRoot.IsBatchLoading = true;
            
            int org = 0;
            int dep = 0;
            int per = 0;
            
            for (int i = 0; i < AmountToGenerate / 450; i++)
            {
                for (int orgNumber = 0; orgNumber < 10; orgNumber++)
                {
                    org++;
                    var Org = new OrganizationTreeModel()
                    {
                        DisplayName = "Organization #" + org.ToString()
                    };
                    

                    for (int departments = 0; departments < 4; departments++)
                    {
                        var department = new OrganizationTreeModel()
                        {
                            DisplayName = "Department #" + (dep++).ToString()
                        };

                        for (int personNumber = 0; personNumber < 10; personNumber++)
                        {
                            var person = new EmployeeTreeModel()
                            {
                                DisplayName = "Employee #" + (per++).ToString(),
                                Position = "Employee",
                                HireDate = DateTime.Now

                            };
                            department.Children.Add(person);
                        }
                        Org.Children.Add(department);
                    }
                    TreeRoot.Children.Add(Org);
                }
            }
            
            TreeRoot.IsBatchLoading = false;
            TreeListModel = TreeRoot;
        }
        
        #region Initialization

        ITreeModelRoot _treeListModel;
        public ITreeModelRoot TreeListModel
        {
            get
            {
                return _treeListModel;
            }
            set
            {
                if (_treeListModel != value)
                {
                    _treeListModel = value;
                    RaisePropertyChanged(() => TreeListModel);
                }
            }
        }


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

        private int _amount = 450;
        public int AmountToGenerate
        {
            get
            {
                return _amount;
            }
            set
            {
                _amount = value;
                LoadData();
            }
        }


        private List<int> _amountList = null;
        public IEnumerable<int> ValidAmounts
        {
            get
            {
                if (_amountList == null)
                {
                    _amountList = new List<int>();
                    _amountList.Add(450);
                    _amountList.Add(4500);
                    _amountList.Add(9000);
                    _amountList.Add(18000);
                    _amountList.Add(36000);
                    _amountList.Add(72000);
                }
                return _amountList;

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
