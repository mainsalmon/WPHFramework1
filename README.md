# WPHFramework1
Caliburn.Micro+MahApps+Autofac Example

.Net 4.5 and VS 2013

This is a demonstration UI app to document various code patterns that would be involved in the UI of a multi-screen application such as would be used for a small data-centric departmental business solution.  It demonstrates the following:

- Integrating Caliburn.Micro and MahApps, and using Autofac for IOC.
- Launching all the various types of MahApps dialogs and capturing return values from them.
- Launching modal and non-modal child windows where the windows are instances of MahApps MetroWindow.
- Windows conducted via the Caliburn Conductor<Screen> mechanism allowing logic to be added to OnActivate() and OnDeactivate().
- Use of Caliburn's EventAggrator for publishing and subscribing to messages.
- An editable datagrid that demonstrates the slick MahApps numeric updown control and overall modern styling by MahApps.
- The (very) basic charting that comes in the WPF DataVisualization Toolkit (in other words, free) with WPF.
- Data entry validation using INotifyDataErrorInfo, including attribute-based validation (via System.ComponentModel.DataAnnotations) and allowance for more complex validation that would involve checking multiple properties at once.
- Async processing with progress tracking and cancellation.
- MahApps Flyout.
- Autofac DI container and injection of all viewmodel dependencies.
- Using MahApps Tiles as a menu bound to actions in the view model via Caliburn conventions.
- Two approaches to busy indicators that are MahApps-friendly. (avoiding toolkit full of controls that are not styled by MahApps).



