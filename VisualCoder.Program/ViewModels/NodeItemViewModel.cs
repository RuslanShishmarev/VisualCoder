using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Commands;
using Prism.Mvvm;
using VisualCoder.API;
using VisualCoder.Program.Models;
using VisualCoder.Program.Views.Components;

namespace VisualCoder.Program.ViewModels
{
    public class NodeItemViewModel : BindableBase
    {
        public DelegateCommand<NodeResult> GetResultValueCommand { get; private set; }
        public DelegateCommand<NodeInput> SetInputValueCommand { get; private set; }

        private NodeItemView _nodeItemView;
        public NodeItemView NodeItemView
        {
            get => _nodeItemView;
            set
            {
                _nodeItemView = value;
                RaisePropertyChanged(nameof(NodeItemView));
            }
        }

        private NodeItem _nodeItem;
        public NodeItem NodeItem
        {
            get => _nodeItem;
            set
            {
                _nodeItem = value;
                RaisePropertyChanged(nameof(NodeItem));
            }
        }

        private MainWindowViewModel _mainContext;

        public NodeItemViewModel(NodeItemView nodeItemView, NodeItem nodeItem, MainWindowViewModel mainContext)
        {
            NodeItemView = nodeItemView;
            NodeItem = nodeItem;
            _mainContext = mainContext;

            GetResultValueCommand = new DelegateCommand<NodeResult>(GetResultValue);
            SetInputValueCommand = new DelegateCommand<NodeInput>(SetInputValue);
        }


        private void GetResultValue(NodeResult result)
        {
            _mainContext.SelectedNodeResult = result;
        }

        private void SetInputValue(NodeInput input)
        {
            _mainContext.SelectedNodeInput = input;
            _mainContext.SelectedNodeInput.Value = _mainContext.SelectedNodeResult?.Value;
        }
    }
}
