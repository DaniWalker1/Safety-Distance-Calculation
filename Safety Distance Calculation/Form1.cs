using Safety_Distance_Calculation.Strategies;
using Safety_Distance_Calculation.Exceptions;
using Safety_Distance_Calculation.Validation;

namespace Safety_Distance_Calculation

{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
            if (cmbApproachType.Items.Count > 0)
            {
                cmbApproachType.SelectedIndex = 0;
            }

            // Set default state for reach-over validation controls
            ToggleReachOverControls();
        }

        private void cmbApproachType_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedApproach = cmbApproachType.SelectedItem?.ToString();

            switch (selectedApproach)
            {
                case "Perpendicular":
                    // S = (K * T) + C. Depends only on T and d.
                    lblExtra.Visible = false;
                    txtExtra.Visible = false;
                    txtExtra.Clear();
                    break;

                case "Parallel":
                    lblExtra.Text = "Height H (mm):";
                    lblExtra.Visible = true;
                    txtExtra.Visible = true;
                    break;

                case "Angle":
                    lblExtra.Text = "Angle α (°):";
                    lblExtra.Visible = true;
                    txtExtra.Visible = true;
                    break;

                default:
                    lblExtra.Visible = false;
                    txtExtra.Visible = false;
                    break;
            }
        }

        private void chkValidateReachOver_CheckedChanged(object sender, EventArgs e)
        {
            ToggleReachOverControls();
        }

        private void ToggleReachOverControls()
        {
            // Enable or disable input fields based on the checkbox state
            bool isEnabled = chkValidateReachOver.Checked;
            txtHazardHeight.Enabled = isEnabled;
            txtGuardHeight.Enabled = isEnabled;

            if (!isEnabled)
            {
                txtHazardHeight.Clear();
                txtGuardHeight.Clear();
            }
        }

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            try
            {
                // 1. Parse base inputs for ISO 13855
                if (!double.TryParse(txtT.Text, out double t) || !double.TryParse(txtD.Text, out double d))
                {
                    throw new FormatException("Please enter valid numeric values for T and d.");
                }

                double extraParam = 0;
                if (txtExtra.Visible && !double.TryParse(txtExtra.Text, out extraParam))
                {
                    throw new FormatException("Please enter a valid numeric value for the Height/Angle.");
                }

                string approachType = cmbApproachType.SelectedItem?.ToString();
                ISafetyDistanceStrategy strategy;
                double safetyDistanceS = 0;

                // 2. Select strategy and calculate Safety Distance (S)
                switch (approachType)
                {
                    case "Perpendicular":
                        strategy = new PerpendicularApproachStrategy();
                        safetyDistanceS = strategy.Calculate(t, d);
                        break;
                    case "Parallel":
                        strategy = new ParallelApproachStrategy();
                        safetyDistanceS = strategy.Calculate(t, d, extraParam);
                        break;
                    case "Angle":
                        strategy = new AngularApproachStrategy();
                        safetyDistanceS = strategy.Calculate(t, d, extraParam);
                        break;
                    default:
                        MessageBox.Show("Please select an approach type.", "Input Required", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                }

                string resultMessage = $"ISO 13855 Safety Distance (S): {safetyDistanceS:F2} mm\n";

                // 3. Execute ISO 13857 validation if requested
                if (chkValidateReachOver.Checked)
                {
                    if (!int.TryParse(txtHazardHeight.Text, out int hazardHeight) || !int.TryParse(txtGuardHeight.Text, out int guardHeight))
                    {
                        throw new FormatException("Please enter valid integer values for Hazard and Guard Heights.");
                    }

                    // Get the required horizontal distance (c) from the matrix
                    int requiredDistanceC = ReachOverValidator.GetRequiredHorizontalDistance(hazardHeight, guardHeight);

                    // Validate if the calculated S is enough
                    bool isSafe = ReachOverValidator.IsSafeFromReachOver(hazardHeight, guardHeight, safetyDistanceS);

                    resultMessage += $"\nISO 13857 Reach Over Validation:\n";
                    resultMessage += $"Required Distance (c): {requiredDistanceC} mm\n";
                    resultMessage += $"Status: {(isSafe ? "SAFE (S >= c)" : "NOT SAFE (S < c)")}";

                    if (!isSafe)
                    {
                        MessageBox.Show(resultMessage, "Safety Validation Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        lblResult.Text = "Status: NOT SAFE";
                        lblResult.ForeColor = System.Drawing.Color.Red;
                        return;
                    }
                }

                // 4. Display final success result
                lblResult.Text = resultMessage;
                lblResult.ForeColor = System.Drawing.Color.DarkGreen;
            }
            catch (SafetyDistanceException ex)
            {
                MessageBox.Show($"Normative violation: {ex.Message}", "Standard Violation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (FormatException ex)
            {
                MessageBox.Show(ex.Message, "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An unexpected error occurred: {ex.Message}", "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
