using System.Collections.Generic;
using System.Windows.Media;
using Metrolib;
using Tailviewer.Count.Ui;
using Tailviewer.QuickInfo.Ui;
using Tailviewer.Ui.Controls.MainPanel.Analyse.Widgets.Help;
using Tailviewer.Ui.Controls.SidePanel;

namespace Tailviewer.Ui.Controls.MainPanel.Analyse.SidePanels
{
	internal sealed class WidgetsSidePanel
		: AbstractSidePanelViewModel
	{
		private readonly List<WidgetFactoryViewModel> _widgets;

		public static readonly string PanelId = "Analysis.Widgets";

		public WidgetsSidePanel()
		{
			_widgets = new List<WidgetFactoryViewModel>
			{
				new WidgetFactoryViewModel(new HelpWidgetPlugin()),
				new WidgetFactoryViewModel(new LogEntryCountWidgetPlugin()),
				new WidgetFactoryViewModel(new QuickInfoWidgetPlugin())
			};
		}

		public IEnumerable<WidgetFactoryViewModel> Widgets => _widgets;

		public override Geometry Icon => Icons.Widgets;

		public override string Id => PanelId;

		public override void Update()
		{
		}
	}
}