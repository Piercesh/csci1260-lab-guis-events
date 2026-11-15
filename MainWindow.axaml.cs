using Avalonia.Controls;
using Avalonia.Interactivity;

namespace GuiEventsLab;

// MainWindow: the code behind the window. It shows the coffee bar and asks
// it to do things. Every decision about orders stays in CoffeeBar.
//
// Part C and Part D are written along with the Session 2 demos.
// Exercise 3 and Exercise 4 are yours.
public partial class MainWindow : Window
{
    private readonly CoffeeBar bar = new CoffeeBar();

    public MainWindow()
    {
        InitializeComponent();

        // TODO: Part C, demo. Subscribe to bar.OrderPlaced.
        bar.AllCaughtUp += OnAllCaughtUp;
        // TODO: Exercise 3. Subscribe to bar.OrderReady and bar.AllCaughtUp.
        UpdateButtons();
        // TODO: Part D, demo. Subscribe to TextChanged on both boxes,
        // then set the buttons for an empty window.

        // TODO: Exercise 4. Connect a Rewards object to the bar and
        // subscribe to its FreeDrinkEarned event.
    }

    // Write the click handlers, the event handlers, and the helper
    // methods below this line.

    private void PlaceButton_Click(object sender, RoutedEventArgs e)
    {
        string customer = CustomerBox.Text;
        string drink = DrinkBox.Text;

        bar.Place(customer, drink);
        UpdateDisplay();
        UpdateButtons();
    }

    private void ReadyButton_Click(object sender, RoutedEventArgs e)
    {
        if (bar.WaitingCount > 0)
        {
            bar.FinishNext();
        }
        UpdateDisplay();
        UpdateButtons();
    }

    private void OnAllCaughtUp(object? sender, EventArgs e)
    {
        StatusText.Text = "All Caught Up";
    }

    private void UpdateDisplay()
    {
        WaitingList.ItemsSource = null;
        WaitingList.ItemsSource = bar.Waiting();

        if (bar.WaitingCount == 0)
        {
            StatusText.Text = "All Caught Up";
        }
        else
        {
            StatusText.Text = $"{bar.WaitingCount} orders waiting";
        }
    }

    private void UpdateButtons()
    {
        PlaceButton.IsEnabled =
            !
            string.IsNullOrWhiteSpace(CustomerBox.Text) && !
            string.IsNullOrWhiteSpace(DrinkBox.Text);
        ReadyButton.IsEnabled = bar.WaitingCount > 0;
    }

    private void Input_TextChanged(object sender, TextChangedEventArgs e)
    {
        UpdateButtons();
    }

}
