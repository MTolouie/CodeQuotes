namespace CodeQuotes;

public partial class MainPage : ContentPage
{
    List<string> Quotes = new();

    public MainPage()
	{
		InitializeComponent();
	}

    protected override void OnAppearing()
    {
        LoadMauiAsset();
    }


    async Task LoadMauiAsset()
    {
        using var stream = await FileSystem.OpenAppPackageFileAsync("quotes.txt");
        using var reader = new StreamReader(stream);

        while (reader.Peek() != -1)
        {
            Quotes.Add(reader.ReadLine());
        }
    }


    private void btnGenerateQuote_Clicked(object sender, EventArgs e)
    {
		var rnd = new Random();

		var startColor = System.Drawing.Color.FromArgb(rnd.Next(0,256), rnd.Next(0, 256), rnd.Next(0, 256));

		var EndColor = System.Drawing.Color.FromArgb(rnd.Next(0,256), rnd.Next(0, 256), rnd.Next(0, 256));


		var colors = ColorUtility.ColorControls.GetColorGradient(startColor,EndColor,6);

        float stopOffset = .0f;
        var stops = new GradientStopCollection();
        foreach (var c in colors)
        {
            stops.Add(new GradientStop(Color.FromArgb(c.Name),
                 stopOffset));
            stopOffset += .2f;
        }

        var gradient =
             new LinearGradientBrush(stops,
                  new Point(0, 0),
                  new Point(1, 1));

        background.Background = gradient;

        var index = rnd.Next(Quotes.Count);
        quote.Text = Quotes[index];

    }
}

