namespace Multi_Control_Event_Interaction
{
    public partial class Form1 : Form
    {
        // Declare custom events
        public event ColorChangedEventHandler ColorChangedEvent;
        public event TextChangedEventHandler TextChangedEvent;

        public Form1()
        {
            InitializeComponent();
            
            // Subscribe multiple methods to ColorChangedEvent (multicast behavior)
            ColorChangedEvent += UpdateLabelColor;
            ColorChangedEvent += ShowNotification;
        
            // Subscribe to TextChangedEvent
            TextChangedEvent += OnTextChanged;
          
            // Set default ComboBox selection
            cmbColors.SelectedIndex = 0;
        }

        // Event handler for btnChangeColor button click
        private void btnChangeColor_Click(object sender, EventArgs e)
        {
            if (cmbColors.SelectedItem != null)
            {
                string colorName = cmbColors.SelectedItem.ToString();
                Color selectedColor = GetColorFromString(colorName);
 
                // Raise the custom ColorChangedEvent with color name
                OnColorChangedEvent(new ColorEventArgs(colorName, selectedColor));
            }
            else
            {
                MessageBox.Show("Please select a color from the ComboBox.", "No Color Selected", 
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // Event handler for btnChangeText button click
        private void btnChangeText_Click(object sender, EventArgs e)
        {
            string newText = DateTime.Now.ToString("dddd, MMMM dd, yyyy HH:mm:ss");
      
            // Raise the custom TextChangedEvent
            OnTextChangedEvent(new TextChangedEventArgs(newText));
        }

        // Method to raise ColorChangedEvent
        protected virtual void OnColorChangedEvent(ColorEventArgs e)
        {
            ColorChangedEvent?.Invoke(this, e);
        }

        // Method to raise TextChangedEvent
        protected virtual void OnTextChangedEvent(TextChangedEventArgs e)
        {
            TextChangedEvent?.Invoke(this, e);
        }

        // Subscriber 1: Updates the label color
        private void UpdateLabelColor(object sender, ColorEventArgs e)
        {
        lblDisplay.ForeColor = e.SelectedColor;
        }

        // Subscriber 2: Shows notification with selected color
        private void ShowNotification(object sender, ColorEventArgs e)
        {
            MessageBox.Show($"Color changed to: {e.ColorName}", 
            "Color Change Notification", 
            MessageBoxButtons.OK, 
            MessageBoxIcon.Information);
        }

        // Event handler for TextChangedEvent
        private void OnTextChanged(object sender, TextChangedEventArgs e)
        {
             lblDisplay.Text = e.NewText;
        }

        // Helper method to convert string to Color
        private Color GetColorFromString(string colorName)
        {
            return colorName switch
            {
                "Red" => Color.Red,
                "Green" => Color.Green,
                "Blue" => Color.Blue,
                _ => Color.Black
            };
        }
    }

    // Custom delegate declarations
    public delegate void ColorChangedEventHandler(object sender, ColorEventArgs e);
    public delegate void TextChangedEventHandler(object sender, TextChangedEventArgs e);

    // Custom EventArgs for ColorChanged event - holds color name and Color object
    public class ColorEventArgs : EventArgs
    {
        public string ColorName { get; set; }
        public Color SelectedColor { get; set; }

        public ColorEventArgs(string colorName, Color color)
        {
            ColorName = colorName;
            SelectedColor = color;
        }
    }

    // Custom EventArgs for TextChanged event
    public class TextChangedEventArgs : EventArgs
    {
        public string NewText { get; set; }

        public TextChangedEventArgs(string text)
        {
            NewText = text;
        }
    }
}
