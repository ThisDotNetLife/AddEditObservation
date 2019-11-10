using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DynamicTableCreation {
    public partial class Default : System.Web.UI.Page {

        // Reference: http://msdn.microsoft.com/en-us/library/kyt0fzt1(v=vs.100).aspx

        override protected void OnInit(EventArgs e) {
            _StopWatch = System.Diagnostics.Stopwatch.StartNew();
            CreateTable(); 
        }

        private System.Diagnostics.Stopwatch _StopWatch;
        private readonly string CHK_NOT_APPLICABLE = "chkNotApplicable";
        private readonly string TXT_NUMERATOR      = "txtNumerator";
        private readonly string TXT_DENOMINATOR    = "txtDenominator";
        private readonly string LBL_COMPLIANT      = "lblCompliant";
        private readonly string LBL_NONCOMPLIANT   = "lblNonCompliant";
        private readonly string BTN_DETAILS        = "btnDetails";
        public readonly string  TABLE_ROW          = "tblRow";
        public readonly string  TABLE_ID_DETAIL    = "tbltracerQuestions";

        public string TracerName     = string.Empty;
        public string TracerCategory = string.Empty;
        public string PageTitle      = string.Empty;

        public string TimeToRenderPage { get; set; }

        protected void Page_Load(object sender, EventArgs e) {
            UpdateTimeToRenderPage();
            if (!Page.IsPostBack) {
                PageTitle = "Add Observation";
            }
        }

        private void CreateTable() {
            var observation = new Observation();
            TracerName     = observation.Name;
            TracerCategory = observation.Category;
            ObservationID.Value = observation.ID.ToString();
            
            Panel1.Controls.Add(new LiteralControl(string.Format("<table id='{0}' style='width:100%;'><tr><td>", "tracerWrapper")));

            CreateTableHeader();
            for (int i = 0; i < observation.Questions.Count; i++) {
                Question ques = observation.Questions[i];
                CreateTableRow(ref ques, i+1, observation.ID);
            }
            CreateTableFooter();

            Panel1.Controls.Add(new LiteralControl("</td></tr></table>"));
        }

        private void CreateTableRow(ref Question pQues, int pRowNo, int pObservationID) {

            // A panel expands to a span (or a div), the Placeholder does not render any tags for itself. 
            // Placeholder controls are good for grouping some items without headache of out HTML tags.
            // Panel controls have the styling capabilites, so you can set the cssclass or style properties 
            // such as background-color, forecolor etc...But Placeholder doesn't have any style attributes 
            // associated. You can not set cssclass or forecolor etc...

            Panel1.Controls.Add(new LiteralControl(string.Format("<tr id='{0}.{1}' style='width: 100%;'>", TABLE_ROW, pRowNo)));

            string clsname = "notRequired";
            string charToPrint = string.Empty;
            if (pQues.IsRequired) {
                clsname = "required";
                charToPrint = "*";
            }
            Panel1.Controls.Add(new LiteralControl(string.Format("<td>{0} <span id='isRequired.{1}' class='{2}'>{3}</span></td>", 
                pQues.No, pQues.No, clsname, charToPrint)));                

            Panel1.Controls.Add(new LiteralControl(string.Format("<td style='width: auto;'>{0}</td>", pQues.Text)));
            Panel1.Controls.Add(new LiteralControl(string.Format("<td style='width: 110px;'>{0}</td>", pQues.EP)));

            Panel1.Controls.Add(new LiteralControl("<td style='text-align:center;'>"));
            Image imgCMS = new Image();
            imgCMS.CssClass = "imgCMS";
            imgCMS.ImageUrl = "content\\images\\cms.png"; 
            imgCMS.Width=32;
            imgCMS.Height=16;
            Panel1.Controls.Add(imgCMS);
            Panel1.Controls.Add(new LiteralControl("</td>"));

            Panel1.Controls.Add(new LiteralControl("<td style='width: 25px;'>"));
            TextBox txtNumerator = new TextBox();
            txtNumerator.ID = string.Format("{0}.{1}", TXT_NUMERATOR, pQues.No);
            txtNumerator.CssClass = "txtDataEntry";
            txtNumerator.Width = 25;
            txtNumerator.MaxLength = 3;
            txtNumerator.Text = pQues.Numerator;
            Panel1.Controls.Add(txtNumerator);
            Panel1.Controls.Add(new LiteralControl("</td>"));

            Panel1.Controls.Add(new LiteralControl("<td style='width: 25px;'>"));
            TextBox txtDenominator = new TextBox();
            txtDenominator.ID = string.Format("{0}.{1}", TXT_DENOMINATOR, pQues.No);
            txtDenominator.CssClass = "txtDataEntry";
            txtDenominator.Width = 25;
            txtDenominator.MaxLength = 3;
            txtDenominator.Text = pQues.Denominator;
            Panel1.Controls.Add(txtDenominator);
            Panel1.Controls.Add(new LiteralControl("</td>"));

            Panel1.Controls.Add(new LiteralControl("<td>"));
            Label lblCompliant = new Label();
            lblCompliant.ID = string.Format("{0}.{1}", LBL_COMPLIANT, pQues.No);
            lblCompliant.CssClass = LBL_COMPLIANT;
            lblCompliant.Text = pQues.Compliant;
            Panel1.Controls.Add(lblCompliant);
            Panel1.Controls.Add(new LiteralControl("</td>"));

            Panel1.Controls.Add(new LiteralControl("<td>"));
            Label lblNonCompliant = new Label();
            lblNonCompliant.ID = string.Format("{0}.{1}", LBL_NONCOMPLIANT, pQues.No);
            lblNonCompliant.CssClass = LBL_NONCOMPLIANT;
            lblNonCompliant.Text = pQues.NonCompliant;
            Panel1.Controls.Add(lblNonCompliant);
            Panel1.Controls.Add(new LiteralControl("</td>"));

            Panel1.Controls.Add(new LiteralControl("<td style='text-align:center;'>"));
            CheckBox chkNotApplicable = new CheckBox();
            chkNotApplicable.ID = string.Format("{0}.{1}", CHK_NOT_APPLICABLE, pQues.No);
            Panel1.Controls.Add(chkNotApplicable);
            Panel1.Controls.Add(new LiteralControl("</td>"));

            Panel1.Controls.Add(new LiteralControl("<td style='text-align:center;'>"));
            Button btnDetails = new Button();
            btnDetails.ID = string.Format("{0}.{1}", BTN_DETAILS, pQues.No);
            btnDetails.Text = "Details";

            string hrefErrorClass = string.Empty;

            if (pObservationID == 0) {
                btnDetails.CssClass = "btnDetailsHide";
                hrefErrorClass      = "hrefErrorHide";
            } else {
                btnDetails.CssClass = "btnDetailsShow";
                hrefErrorClass      = "hrefErrorShow";
            }
            Panel1.Controls.Add(btnDetails);
            Panel1.Controls.Add(new LiteralControl("</td>"));

            Panel1.Controls.Add(new LiteralControl(
                string.Format("<td style='text-align:center;'><a href='#' id='hrefErr.{0}' class='{1}'>Error</td>", 
                    pQues.No, hrefErrorClass)));

            Panel1.Controls.Add(new LiteralControl("</tr>"));
        }

        private void CreateTableHeader() {
            Panel1.Controls.Add(new LiteralControl(string.Format("<table id='{0}' style='width: 100%;'>", "tracerQuestions")));
            Panel1.Controls.Add(new LiteralControl("<thead id='tracerQuestionsHeader'>"));
            Panel1.Controls.Add(new LiteralControl("<td style='width: 35px;'>Q #</td>"));
            Panel1.Controls.Add(new LiteralControl("<td style='width: auto; text-align:center;'>Question Text</td>"));

            Panel1.Controls.Add(new LiteralControl("<td style='width: 142px; text-align:center;' colspan='2'>Standard - EP</td>"));
            Panel1.Controls.Add(new LiteralControl("<td style='width: 30px; text-align:center;'>Num.</td>"));
            Panel1.Controls.Add(new LiteralControl("<td style='width: 30px; text-align:center;'>Den.</td>"));
            Panel1.Controls.Add(new LiteralControl("<td style='width: 30px; text-align:center;'>Cmp.</td>"));
            Panel1.Controls.Add(new LiteralControl("<td style='width: 40px; text-align:center;'>N&nbsp;Cmp.</td>"));
            Panel1.Controls.Add(new LiteralControl("<td style='width: 20px; text-align:center;'>N/A</br>"));

            CheckBox chkToggleAll = new CheckBox();
            chkToggleAll.ID = "chkToggleAll";
            Panel1.Controls.Add(chkToggleAll);

            Panel1.Controls.Add(new LiteralControl("</td>"));

            Panel1.Controls.Add(new LiteralControl("<td style='width: 60px; text-align:center;'>Details</td>"));
            Panel1.Controls.Add(new LiteralControl("<td style='width: 50px; text-align:center;'>Error ?</td>"));
            Panel1.Controls.Add(new LiteralControl("</thead>"));
            Panel1.Controls.Add(new LiteralControl("<tbody>"));
        }       

        private void CreateTableFooter() {
            Panel1.Controls.Add(new LiteralControl("</tbody>"));
            Panel1.Controls.Add(new LiteralControl("</table>"));
        }

        protected void btnSave_Click(object sender, EventArgs e) {
            System.Collections.Specialized.NameValueCollection postedValues = Request.Form;

            int noOfQuestions = Convert.ToInt32(NoOfQuestions.Value);

            if (ObservationID.Value == "0") {
                // TODO Save Observation to DB. Assign the Observation ID to the hidden form field.            
                ObservationID.Value = "5";

                // When observation is F-I-R-S-T saved, set CSS class so all detail buttons are displayed.
                PageTitle = "Edit Observation";
                for (int i = 0; i < noOfQuestions; i++) {
                    string keyDetails = string.Format("{0}.{1}", BTN_DETAILS, i + 1);
                    Button btnDetails = (Button)FindControl(keyDetails);
                    btnDetails.CssClass = "btnDetailsShow";
                }
            } else {
                // TODO Update data entry into DB.
            }

            for (int i = 0; i < noOfQuestions; i++) {
                string keyNumerator    = string.Format("{0}.{1}", TXT_NUMERATOR,  i+1);
                string keyDemominator  = string.Format("{0}.{1}", TXT_DENOMINATOR, i+1);
                string keyIsApplicable = string.Format("{0}.{1}", CHK_NOT_APPLICABLE, i+1);

                int numerator = 0;
                int denominator = 0;

                bool isNumeratorNumeric   = true;
                bool isDenominatorNumeric = true;

                string isApplicable = "off";

                isNumeratorNumeric   = int.TryParse(Request.Form[keyNumerator].ToString(), out numerator);
                isDenominatorNumeric = int.TryParse(Request.Form[keyDemominator].ToString(), out denominator);

                // Interesting Find: If a checkbox is --NOT-- checked on, ASP.NET will not return the checkbox
                // in the Request.Form NameValueCollection.
                if (postedValues[keyIsApplicable] != null) {
                    isApplicable=Request.Form[keyIsApplicable].ToString();
                }
                // Echo output to verify we can access data.
                // System.Diagnostics.Debug.WriteLine("Ques: {0:D2} Is Applicable: {1} Numerator: {2} Denonimator: {3}",
                //     i+1, isApplicable, numerator, denominator);

                // Increment values in table to prove we can access the data.
                //if (isApplicable == "on") {
                //    int newValue = 0;
                //    TextBox ctlNumerator = (TextBox)FindControl(keyNumerator);
                //    if (!string.IsNullOrEmpty(ctlNumerator.Text)){
                //        newValue = Convert.ToInt32(ctlNumerator.Text) + 1;
                //        ctlNumerator.Text = newValue.ToString();
                //    }

                //    TextBox ctlDenominator = (TextBox)FindControl(keyDemominator);
                //    if (!string.IsNullOrEmpty(ctlDenominator.Text)) {
                //        newValue = Convert.ToInt32(ctlDenominator.Text) + 2;
                //        ctlDenominator.Text = newValue.ToString();
                //    }
                //}
            }
            UpdateTimeToRenderPage();
        }

        protected void btnMarkAsCompleted_Click(object sender, EventArgs e) {
            System.Collections.Specialized.NameValueCollection postedValues = Request.Form;

            int noOfQuestions = Convert.ToInt32(NoOfQuestions.Value);

            for (int i = 0; i < noOfQuestions; i++) {
                string keyNumerator = string.Format("{0}.{1}", TXT_NUMERATOR, i + 1);
                string keyDemominator = string.Format("{0}.{1}", TXT_DENOMINATOR, i + 1);
                string keyIsApplicable = string.Format("{0}.{1}", CHK_NOT_APPLICABLE, i + 1);

                int numerator = 0;
                int denominator = 0;

                bool isNumeratorNumeric = true;
                bool isDenominatorNumeric = true;

                string isApplicable = "off";

                isNumeratorNumeric = int.TryParse(Request.Form[keyNumerator].ToString(), out numerator);
                isDenominatorNumeric = int.TryParse(Request.Form[keyDemominator].ToString(), out denominator);

                // Interesting Find: If a checkbox is --NOT-- checked on, ASP.NET will not return the checkbox
                // in the Request.Form NameValueCollection.
                if (postedValues[keyIsApplicable] != null) {
                    isApplicable = Request.Form[keyIsApplicable].ToString();
                }
                // Echo output to verify we can access data.
                // System.Diagnostics.Debug.WriteLine("Ques: {0:D2} Is Applicable: {1} Numerator: {2} Denonimator: {3}",
                //     i+1, isApplicable, numerator, denominator);

                // Increment values in table to prove we can access the data.
                //if (isApplicable == "on") {
                //    int newValue = 0;
                //    TextBox ctlNumerator = (TextBox)FindControl(keyNumerator);
                //    if (!string.IsNullOrEmpty(ctlNumerator.Text)){
                //        newValue = Convert.ToInt32(ctlNumerator.Text) + 1;
                //        ctlNumerator.Text = newValue.ToString();
                //    }

                //    TextBox ctlDenominator = (TextBox)FindControl(keyDemominator);
                //    if (!string.IsNullOrEmpty(ctlDenominator.Text)) {
                //        newValue = Convert.ToInt32(ctlDenominator.Text) + 2;
                //        ctlDenominator.Text = newValue.ToString();
                //    }
                //}
            }
            UpdateTimeToRenderPage();
        }

        private void UpdateTimeToRenderPage() {
            long ms = _StopWatch.ElapsedMilliseconds;
            TimeToRenderPage = string.Format("{0} milliseconds.", ms);
        }
    }
}