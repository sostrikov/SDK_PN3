using System;
using System.Text;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using System.Diagnostics;

using ParsecIntegrationClient.IntegrationWebService;

namespace ParsecIntegrationClient
{
    public class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
		//Регистрация
        private void btnStartSession_Click(object sender, EventArgs e)
        {
            ClearResult();
            txtCommonSessionID.Text = "Wait";
            IntegrationService integrService = new IntegrationService();
            string domain = txtDomain.Text;
            string userName = txtLogin.Text;
            string password = txtPassword.Text;
            SessionResult res = integrService.OpenSession(domain, userName, password);
            ShowResult(res);
            txtCommonSessionID.Text = res.Value.SessionID.ToString();
            txtOrgUnitID.Text = res.Value.RootOrgUnitID.ToString();
            txtTerraID.Text = res.Value.RootTerritoryID.ToString();
        }

        private void btnContinuSession_Click(object sender, EventArgs e)
        {
            txtContinueSessionRes.Text = "Wait";
            IntegrationService integrService = new IntegrationService();
            Guid sessionID = new Guid(txtCommonSessionID.Text);
            int res = integrService.ContinueSession(sessionID);
            txtContinueSessionRes.Text = res.ToString();
        }

        private void btnGetRootOrgUnit_Click(object sender, EventArgs e)
        {
            //txtOrgUnitID.Text = "Wait";
            //IntegrationService integrService = new IntegrationService();
            //Guid sessionID = new Guid(txtCommonSessionID.Text);
            //GuidResult res = integrService.GetRootOrgUnit(sessionID);
            //txtOrgUnitID.Text = res.Value.ToString();
        }

        private void btnGetOrgUnit_Click(object sender, EventArgs e)
        {
            IntegrationService integrService = new IntegrationService();
            Guid sessionID = new Guid(txtCommonSessionID.Text);
            Guid orgUnitID = new Guid(txtOrgUnitID.Text);
            OrgUnit res = integrService.GetOrgUnit(sessionID, orgUnitID);
            Debug.WriteLine(string.Format("GetOrgUnit resulted ID:{0};NAME:{1};DESC:{2};PARENT_ID:{3};", res.ID, res.NAME, res.DESC, res.PARENT_ID));
            pgObjectData.SelectedObject = res;
            pgObjectData.Refresh();
        }

        private void btnGetOrgUnitSubs_Click(object sender, EventArgs e)
        {
            IntegrationService integrService = new IntegrationService();
            Guid sessionID = new Guid(txtCommonSessionID.Text);
            Guid orgUnitID = new Guid(txtOrgUnitID.Text);
            BaseObject[] res = integrService.GetOrgUnitSubItems(sessionID, orgUnitID);
            if (res == null)
                Debug.WriteLine(string.Format("GetOrgUnitSubItems resulted :null;"));
            else
            {
                for (int i = 0; i < res.Length; i++)
                {
                    BaseOrgUnit bOrgUnit = res[i] as BaseOrgUnit;
                    if (bOrgUnit != null)
                    {
                        Debug.WriteLine(string.Format("GetOrgUnitSubItems i:{0}; OrgUnit ID:{1};NAME:{2};DESC:{3};", i, bOrgUnit.ID, bOrgUnit.NAME, bOrgUnit.DESC));
                        continue;
                    }
                    BasePerson bPerson = res[i] as BasePerson;
                    if (bPerson != null)
                    {
                        Debug.WriteLine(string.Format("GetOrgUnitSubItems i:{0}; Person ID:{1};LAST_NAME:{2};FIRST_NAME:{3};MIDDLE_NAME:{4};TAB_NUM:{5};", i, bPerson.ID, bPerson.LAST_NAME, bPerson.FIRST_NAME, bPerson.MIDDLE_NAME, bPerson.TAB_NUM));
                        continue;
                    }
                    Debug.WriteLine(string.Format("GetOrgUnitSubItems i:{0}; unknown", i));
                }
            }
        }

        private void btnGetPerson_Click(object sender, EventArgs e)
        {
            IntegrationService integrService = new IntegrationService();
            Guid sessionID = new Guid(txtCommonSessionID.Text);
            Guid persID = new Guid(txtPersID.Text);
            PersonWithPhoto res = integrService.GetPerson(sessionID, persID);
            pgObjectData.SelectedObject = res;
            pgObjectData.Refresh();
            if (res == null)
            {
                Debug.WriteLine(string.Format("GetPerson null"));
            }
            else
            {
                Debug.WriteLine(string.Format("GetPerson Person ID:{0};LAST_NAME:{1};FIRST_NAME:{2};MIDDLE_NAME:{3};TAB_NUM:{4};ORG_ID:{5};", res.ID, res.LAST_NAME, res.FIRST_NAME, res.MIDDLE_NAME, res.TAB_NUM, res.ORG_ID));
                byte[] photoArr = res.PHOTO;
                if (photoArr == null || photoArr.Length == 0)
                    pbPersPhoto.Image = null;
                else
                {
                    MemoryStream ms = new MemoryStream(photoArr);
                    pbPersPhoto.Image = Image.FromStream(ms);
                }
            }
        }

        private void btnGetAccList_Click(object sender, EventArgs e)
        {
            IntegrationService integrService = new IntegrationService();
            Guid sessionID = new Guid(txtCommonSessionID.Text);
            AccessGroup[] res = integrService.GetAccessGroups(sessionID);
            pgObjectData.SelectedObject = res;
            pgObjectData.Refresh();
            if (res == null)
                Debug.WriteLine(string.Format("GetAccessGroups resulted :null;"));
            else if (res.Length == 0)
                Debug.WriteLine(string.Format("GetAccessGroups resulted :empty;"));
            else
                for (int i = 0; i < res.Length; i++)
                {
                    AccessGroup accessGroup = res[i];
                    if (accessGroup != null)
                        Debug.WriteLine(string.Format("GetAccessGroups i:{0}; ID:{1};NAME:{2};", i, accessGroup.ID, accessGroup.NAME));
                    else
                        Debug.WriteLine(string.Format("GetAccessGroups i:{0}; null", i));
                }
        }

        private void btnGetPersonIdentifiers_Click(object sender, EventArgs e)
        {
            IntegrationService integrService = new IntegrationService();
            Guid sessionID = new Guid(txtCommonSessionID.Text);
            Guid persID = new Guid(txtPersID.Text);
            Identifier[] res = integrService.GetPersonIdentifiers(sessionID, persID);
            pgObjectData.SelectedObject = res;
            pgObjectData.Refresh();
            if (res == null)
                Debug.WriteLine(string.Format("GetPersonIdentifiers resulted :null;"));
            else if (res.Length == 0)
                Debug.WriteLine(string.Format("GetPersonIdentifiers resulted :empty;"));
            else
            {
                for (int i = 0; i < res.Length; i++)
                {
                    IdentifierTemp identifier = res[i] as IdentifierTemp;
                    if (identifier != null)
                        Debug.WriteLine(string.Format("GetPersonIdentifiers i:{0}; CODE:{1};IS_PRIMARY:{2};ACCGROUP_ID:{3};VALID_FROM:{4};VALID_TO:{5};", i, identifier.CODE, identifier.IS_PRIMARY, identifier.ACCGROUP_ID, identifier.VALID_FROM, identifier.VALID_TO));
                    else if (res[i] == null)
                        Debug.WriteLine(string.Format("GetPersonIdentifiers i:{0}; null", i));
                    else
                        Debug.WriteLine(string.Format("GetPersonIdentifiers i:{0}; CODE:{1};IS_PRIMARY:{2};ACCGROUP_ID:{3};", i, res[i].CODE, res[i].IS_PRIMARY, res[i].ACCGROUP_ID));
                }
            }
        }

        private void btnGetOrgHierarhy_Click(object sender, EventArgs e)
        {
            IntegrationService integrService = new IntegrationService();
            Guid sessionID = new Guid(txtCommonSessionID.Text);
            OrgUnit[] res = integrService.GetOrgUnitsHierarhy(sessionID);
            pgObjectData.SelectedObject = res;
            pgObjectData.Refresh();
            if (res == null)
                Debug.WriteLine(string.Format("GetOrgUnitsHierarhy resulted :null;"));
            else if (res.Length == 0)
                Debug.WriteLine(string.Format("GetOrgUnitsHierarhy resulted :empty;"));
            else
                for (int i = 0; i < res.Length; i++)
                {
                    OrgUnit orgUnit = res[i];
                    if (orgUnit != null)
                        Debug.WriteLine(string.Format("GetOrgUnitsHierarhy i:{0}; ID:{1};NAME:{2};DESC:{3};PARENT_ID:{4};", i, orgUnit.ID, orgUnit.NAME, orgUnit.DESC, orgUnit.PARENT_ID));
                    else
                        Debug.WriteLine(string.Format("GetOrgUnitsHierarhy i:{0}; null", i));
                }
        }

        private void btnGetFullHierarhy_Click(object sender, EventArgs e)
        {
            IntegrationService integrService = new IntegrationService();
            Guid sessionID = new Guid(txtCommonSessionID.Text);
            BaseObject[] res = integrService.GetOrgUnitsHierarhyWithPersons(sessionID);
            if (res == null)
                Debug.WriteLine(string.Format("GetOrgUnitsHierarhyWithPersons resulted :null;"));
            else
            {
                for (int i = 0; i < res.Length; i++)
                {
                    OrgUnit bOrgUnit = res[i] as OrgUnit;
                    if (bOrgUnit != null)
                    {
                        Debug.WriteLine(string.Format("GetOrgUnitsHierarhyWithPersons i:{0}; OrgUnit ID:{1};NAME:{2};DESC:{3};PARENT_ID:{4};", i, bOrgUnit.ID, bOrgUnit.NAME, bOrgUnit.DESC, bOrgUnit.PARENT_ID));
                        continue;
                    }
                    Person bPerson = res[i] as Person;
                    if (bPerson != null)
                    {
                        Debug.WriteLine(string.Format("GetOrgUnitsHierarhyWithPersons i:{0}; Person ID:{1};LAST_NAME:{2};FIRST_NAME:{3};MIDDLE_NAME:{4};TAB_NUM:{5};ORG_ID:{6};", i, bPerson.ID, bPerson.LAST_NAME, bPerson.FIRST_NAME, bPerson.MIDDLE_NAME, bPerson.TAB_NUM, bPerson.ORG_ID));
                        continue;
                    }
                    Debug.WriteLine(string.Format("GetOrgUnitsHierarhyWithPersons i:{0}; unknown", i));
                }
            }
        }

        private void pbPersPhoto_DoubleClick(object sender, EventArgs e)
        {
            // change photo
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = CommonFunctions.GetImageCodecsFilesFilter(ImageCodecInfo.GetImageDecoders(), true);
            ofd.FilterIndex = 1;
            ofd.Multiselect = false;
            ofd.RestoreDirectory = true;
            ofd.CheckFileExists = true;
            ofd.CheckPathExists = true;
            if (ofd.ShowDialog(this) == DialogResult.OK)
                pbPersPhoto.Image = CommonFunctions.LoadFromFile(ofd.FileName);
        }

        private void btnCreateOrgUnit_Click(object sender, EventArgs e)
        {
            ClearResult();
            OrgUnit toSave = pgObjectData.SelectedObject as OrgUnit;
            if (toSave == null)
                return;
            Guid SessionID = new Guid(txtCommonSessionID.Text);
            IntegrationService integrService = new IntegrationService();
            GuidResult res = integrService.CreateOrgUnit(SessionID, toSave);
            txtOrgUnitID.Text = res.Value.ToString();
            ShowResult(res);
        }

        private void btnSaveOrgUnit_Click(object sender, EventArgs e)
        {
            ClearResult();
            OrgUnit toSave = pgObjectData.SelectedObject as OrgUnit;
            if (toSave == null)
                return;
            Guid editSessionID = new Guid(txtEditSessionID.Text);
            IntegrationService integrService = new IntegrationService();
            BaseResult res = integrService.SaveOrgUnit(editSessionID, toSave);
            ShowResult(res);
        }

        private void btnSaveOrgUnitShort_Click(object sender, EventArgs e)
        {
            ClearResult();
            BaseOrgUnit curOrgUnit = pgObjectData.SelectedObject as BaseOrgUnit;
            if (curOrgUnit == null)
                return;
            BaseOrgUnit toSave = new BaseOrgUnit();
            toSave.ID = curOrgUnit.ID;
            toSave.NAME = curOrgUnit.NAME;
            toSave.DESC = curOrgUnit.DESC;

            Guid editSessionID = new Guid(txtEditSessionID.Text);
            IntegrationService integrService = new IntegrationService();
            BaseResult res = integrService.SaveOrgUnit(editSessionID, toSave);
            ShowResult(res);
        }

        private void btnEditOrgUnit_Click(object sender, EventArgs e)
        {
            ClearResult();
            Guid sessionID = new Guid(txtCommonSessionID.Text);
            Guid orgUnitID = new Guid(txtOrgUnitID.Text);
            IntegrationService integrService = new IntegrationService();
            GuidResult res = integrService.OpenOrgUnitEditingSession(sessionID, orgUnitID);
            txtEditSessionID.Text = res.Value.ToString();
            ShowResult(res);
        }

        private void btnDeleteOrgUnit_Click(object sender, EventArgs e)
        {
            ClearResult();
            Guid sessionID = new Guid(txtCommonSessionID.Text);
            Guid orgUnitID = new Guid(txtOrgUnitID.Text);
            IntegrationService integrService = new IntegrationService();
            BaseResult res = integrService.DeleteOrgUnit(sessionID, orgUnitID);
            ShowResult(res);
        }

        private void btnCreatePerson_Click(object sender, EventArgs e)
        {
            ClearResult();
            Person curPerson = pgObjectData.SelectedObject as Person;
            if (curPerson == null)
                return;
            PersonWithPhoto toSave = new PersonWithPhoto();
            toSave.ID = curPerson.ID;
            toSave.LAST_NAME = curPerson.LAST_NAME;
            toSave.FIRST_NAME = curPerson.FIRST_NAME;
            toSave.MIDDLE_NAME = curPerson.MIDDLE_NAME;
            toSave.TAB_NUM = curPerson.TAB_NUM;
            toSave.ORG_ID = curPerson.ORG_ID;
            toSave.PHOTO = CommonFunctions.GetPhotoArray(pbPersPhoto.Image);
            Guid sessionID = new Guid(txtCommonSessionID.Text);
            IntegrationService integrService = new IntegrationService();
            GuidResult res = integrService.CreatePerson(sessionID, toSave);
            txtPersID.Text = res.Value.ToString();
            ShowResult(res);
        }

        private void btnSavePerson_Click(object sender, EventArgs e)
        {
            ClearResult();
            PersonWithPhoto toSave = pgObjectData.SelectedObject as PersonWithPhoto;
            if (toSave == null)
                return;
            // set photo property
            toSave.PHOTO = CommonFunctions.GetPhotoArray(pbPersPhoto.Image);
            Guid editSessionID = new Guid(txtEditSessionID.Text);
            IntegrationService integrService = new IntegrationService();
            BaseResult res = integrService.SavePerson(editSessionID, toSave);
            ShowResult(res);
        }

        private void btnSavePersonShort_Click(object sender, EventArgs e)
        {
            ClearResult();
            BasePerson curPerson = pgObjectData.SelectedObject as BasePerson;
            if (curPerson == null)
                return;
            BasePerson toSave = new BasePerson();
            toSave.ID = curPerson.ID;
            toSave.LAST_NAME = curPerson.LAST_NAME;
            toSave.FIRST_NAME = curPerson.FIRST_NAME;
            toSave.MIDDLE_NAME = curPerson.MIDDLE_NAME;
            toSave.TAB_NUM = curPerson.TAB_NUM;
            Guid editSessionID = new Guid(txtEditSessionID.Text);
            IntegrationService integrService = new IntegrationService();
            BaseResult res = integrService.SavePerson(editSessionID, toSave);
            ShowResult(res);
        }

        private void btnSetPhoto_Click(object sender, EventArgs e)
        {
            ClearResult();
            Guid editSessionID = new Guid(txtEditSessionID.Text);
            byte[] imageArray = CommonFunctions.GetPhotoArray(pbPersPhoto.Image);
            IntegrationService integrService = new IntegrationService();
            BaseResult res = integrService.SetPersonPhoto(editSessionID, imageArray);
            ShowResult(res);
        }

        private void btnSetOrgUnit_Click(object sender, EventArgs e)
        {
            ClearResult();
            Guid editSessionID = new Guid(txtEditSessionID.Text);
            Guid orgUnitID = new Guid(txtOrgUnitID.Text);
            IntegrationService integrService = new IntegrationService();
            BaseResult res = integrService.SetPersonOrgUnit(editSessionID, orgUnitID);
            ShowResult(res);
        }

        private void btnDeletePerson_Click(object sender, EventArgs e)
        {
            ClearResult();
            Guid sessionID = new Guid(txtCommonSessionID.Text);
            Guid persID = new Guid(txtPersID.Text);
            IntegrationService integrService = new IntegrationService();
            BaseResult res = integrService.DeletePerson(sessionID, persID);
            ShowResult(res);
        }

        private void btnGetIdentifier_Click(object sender, EventArgs e)
        {
            ClearResult();
            Guid sessionID = new Guid(txtCommonSessionID.Text);
            Guid persID = new Guid(txtPersID.Text);
            IntegrationService integrService = new IntegrationService();
            Identifier[] idents = integrService.GetPersonIdentifiers(sessionID, persID);

            pgObjectData.SelectedObject = (idents == null || idents.Length == 0) ? null : idents[0];
            pgObjectData.Refresh();
        }

        private void btnIdentifBeginEditSession_Click(object sender, EventArgs e)
        {
            ClearResult();
            Guid sessionID = new Guid(txtCommonSessionID.Text);
            Guid persID = new Guid(txtPersID.Text);
            IntegrationService integrService = new IntegrationService();
            GuidResult res = integrService.OpenPersonEditingSession(sessionID, persID);
            txtEditSessionID.Text = res.Value.ToString();
            ShowResult(res);
        }

        private void btnEndEditIdent_Click(object sender, EventArgs e)
        {
            ClearResult();
            Guid editSessionID = new Guid(txtEditSessionID.Text);
            IntegrationService integrService = new IntegrationService();
            integrService.ClosePersonEditingSession(editSessionID);
        }

        private void btnCreateIdent_Click(object sender, EventArgs e)
        {
            ClearResult();
            Guid editSessionID = new Guid(txtEditSessionID.Text);
            Identifier identObject = pgObjectData.SelectedObject as Identifier;
            if (identObject == null)
                return;
            BaseIdentifier bi = null;
            if (chkIsTemp.Checked)
            {
                IdentifierTemp tempI = new IdentifierTemp();
                tempI.ACCGROUP_ID = identObject.ACCGROUP_ID;
                tempI.VALID_FROM = dtpFrom.Value;
                tempI.VALID_TO = dtpTo.Value;
                bi = tempI;
            }
            else
            {
                bi = new BaseIdentifier();
            }
            bi.CODE = identObject.CODE;
            bi.IS_PRIMARY = identObject.IS_PRIMARY;
            IntegrationService integrService = new IntegrationService();
            BaseResult res = integrService.AddPersonIdentifier(editSessionID, bi);
            ShowResult(res);
        }

        private void btnSaveIdent_Click(object sender, EventArgs e)
        {
            ClearResult();
            Guid editSessionID = new Guid(txtEditSessionID.Text);
            Identifier identObject = pgObjectData.SelectedObject as Identifier;
            if (identObject == null)
                return;
            Identifier bi = new Identifier();
            if (chkIsTemp.Checked)
            {
                IdentifierTemp tempI = new IdentifierTemp();
                tempI.VALID_FROM = dtpFrom.Value;
                tempI.VALID_TO = dtpTo.Value;
                bi = tempI;
            }
            else
            {
                bi = new Identifier();
            }
            bi.CODE = identObject.CODE;
            bi.IS_PRIMARY = identObject.IS_PRIMARY;
            bi.ACCGROUP_ID = identObject.ACCGROUP_ID;
            IntegrationService integrService = new IntegrationService();
            BaseResult res = integrService.ChangePersonIdentifier(editSessionID, bi);
            ShowResult(res);
        }

        private void btnDeleteIdent_Click(object sender, EventArgs e)
        {
            ClearResult();
            Guid sessionID = new Guid(txtCommonSessionID.Text);
            string identID = txtIdentiID.Text;
            IntegrationService integrService = new IntegrationService();
            BaseResult res = integrService.DeleteIdentifier(sessionID, identID);
            ShowResult(res);
        }

        private void btnGetRootTerritory_Click(object sender, EventArgs e)
        {
            //ClearResult();
            //Guid sessionID = new Guid(txtCommonSessionID.Text);
            //IntegrationService integrService = new IntegrationService();
            //GuidResult res = integrService.GetRootTerritory(sessionID);
            //txtTerraID.Text = res.Value.ToString();
            //ShowResult(res);
        }

        private void btnGetTerraHierarhy_Click(object sender, EventArgs e)
        {
            ClearResult();
            Guid sessionID = new Guid(txtCommonSessionID.Text);
            IntegrationService integrService = new IntegrationService();
            Territory[] terras = integrService.GetTerritoriesHierarhy(sessionID);
            if (terras != null && terras.Length > 0)
            {
                for (int i = 0; i < terras.Length; i++)
                {
                    Territory terra = terras[i];
                    if (terra == null)
                        Debug.WriteLine(string.Format("GetTerritoriesHierarhy i:{0}; null", i));
                    else
                        Debug.WriteLine(string.Format("GetTerritoriesHierarhy i:{0}; ID:{1};NAME:{2};DESC:{3};TYPE:{4};PARENT_ID:{5};", i, terra.ID, terra.NAME, terra.DESC, terra.TYPE, terra.PARENT_ID));
                }
            }
            else
            {
                Debug.WriteLine(string.Format("GetTerritoriesHierarhy empty"));
            }
        }

        private void btnGetEvents_Click(object sender, EventArgs e)
        {
            ClearResult();
            Guid sessionID = new Guid(txtCommonSessionID.Text);
            Guid persID = new Guid(txtPersID.Text);
            Guid terraID = new Guid(txtTerraID.Text);
            DateTime from = dtpFrom.Value;
            DateTime to = dtpTo.Value;
            IntegrationService integrService = new IntegrationService();
            EventsHistoryResult res = integrService.GetEvents(sessionID, terraID, persID, from, to);
            EventsHistory history = res.Value;
            if (history != null)
            {
                Event[] events = history.Events;
                if (events != null && events.Length > 0)
                {
                    for (int i = 0; i < events.Length; i++)
                    {
                        Event ev = events[i];
                        if (ev == null)
                            Debug.WriteLine(string.Format("GetEvents i:{0}; null", i));
                        else
                            Debug.WriteLine(string.Format("GetEvents i:{0}; EventDate:{1}; EventType:{2}; CODE:{3};PERS_ID:{4};PERS_NAME:{5};TERRA_ID:{6};TERRA_NAME{7}", i, ev.EventDate, ev.EventType, ev.CODE, history.Persons[ev.EventPersonIndex], history.PersonFullNames[ev.EventPersonIndex], history.Territories[ev.EventTerritoryIndex], history.TerritoryNames[ev.EventTerritoryIndex]));
                    }
                    Debug.WriteLine(string.Format("GetEvents Total:{0};", events.Length));
                }
                else
                {
                    Debug.WriteLine(string.Format("GetEvents empty"));
                }
            }
            else
            {
                Debug.WriteLine(string.Format("GetEvents null"));
            }
            ShowResult(res);
        }

        private void ClearResult()
        {
            txtRes.Text = string.Empty;
            txtErrMess.Text = string.Empty;
        }

        private void ShowResult(BaseResult res)
        {
            txtRes.Text = res.Result.ToString();
            txtErrMess.Text = res.ErrorMessage;
        }

		private Button button1;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
			this.txtDomain = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.txtLogin = new System.Windows.Forms.TextBox();
			this.txtPassword = new System.Windows.Forms.TextBox();
			this.btnStartSession = new System.Windows.Forms.Button();
			this.btnContinuSession = new System.Windows.Forms.Button();
			this.txtContinueSessionRes = new System.Windows.Forms.TextBox();
			this.btnGetRootOrgUnit = new System.Windows.Forms.Button();
			this.txtCommonSessionID = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.txtOrgUnitID = new System.Windows.Forms.TextBox();
			this.btnGetOrgUnit = new System.Windows.Forms.Button();
			this.btnGetOrgUnitSubs = new System.Windows.Forms.Button();
			this.txtPersID = new System.Windows.Forms.TextBox();
			this.label9 = new System.Windows.Forms.Label();
			this.pgObjectData = new System.Windows.Forms.PropertyGrid();
			this.btnGetPerson = new System.Windows.Forms.Button();
			this.pbPersPhoto = new System.Windows.Forms.PictureBox();
			this.btnGetAccList = new System.Windows.Forms.Button();
			this.btnGetPersonIdentifiers = new System.Windows.Forms.Button();
			this.btnGetOrgHierarhy = new System.Windows.Forms.Button();
			this.label4 = new System.Windows.Forms.Label();
			this.txtEditSessionID = new System.Windows.Forms.TextBox();
			this.btnEditOrgUnit = new System.Windows.Forms.Button();
			this.btnSaveOrgUnit = new System.Windows.Forms.Button();
			this.btnDeleteOrgUnit = new System.Windows.Forms.Button();
			this.btnSavePerson = new System.Windows.Forms.Button();
			this.btnSavePersonShort = new System.Windows.Forms.Button();
			this.btnSetPhoto = new System.Windows.Forms.Button();
			this.btnDeletePerson = new System.Windows.Forms.Button();
			this.btnSetOrgUnit = new System.Windows.Forms.Button();
			this.btnCreateOrgUnit = new System.Windows.Forms.Button();
			this.btnCreatePerson = new System.Windows.Forms.Button();
			this.txtRes = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.txtErrMess = new System.Windows.Forms.TextBox();
			this.btnSaveOrgUnitShort = new System.Windows.Forms.Button();
			this.label6 = new System.Windows.Forms.Label();
			this.txtIdentiID = new System.Windows.Forms.TextBox();
			this.btnIdentifBeginEditSession = new System.Windows.Forms.Button();
			this.btnEndEditIdent = new System.Windows.Forms.Button();
			this.btnCreateIdent = new System.Windows.Forms.Button();
			this.btnSaveIdent = new System.Windows.Forms.Button();
			this.btnDeleteIdent = new System.Windows.Forms.Button();
			this.btnGetIdentifier = new System.Windows.Forms.Button();
			this.chkIsTemp = new System.Windows.Forms.CheckBox();
			this.dtpFrom = new System.Windows.Forms.DateTimePicker();
			this.dtpTo = new System.Windows.Forms.DateTimePicker();
			this.label10 = new System.Windows.Forms.Label();
			this.txtTerraID = new System.Windows.Forms.TextBox();
			this.btnGetRootTerritory = new System.Windows.Forms.Button();
			this.btnGetTerraHierarhy = new System.Windows.Forms.Button();
			this.btnGetEvents = new System.Windows.Forms.Button();
			this.btnGetFullHierarhy = new System.Windows.Forms.Button();
			this.button1 = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.pbPersPhoto)).BeginInit();
			this.SuspendLayout();
			// 
			// txtDomain
			// 
			this.txtDomain.Location = new System.Drawing.Point(76, 47);
			this.txtDomain.Name = "txtDomain";
			this.txtDomain.Size = new System.Drawing.Size(100, 20);
			this.txtDomain.TabIndex = 0;
			this.txtDomain.Text = "SYSTEM";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 50);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(43, 13);
			this.label1.TabIndex = 2;
			this.label1.Text = "Domain";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(12, 74);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(33, 13);
			this.label2.TabIndex = 3;
			this.label2.Text = "Login";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(12, 101);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(53, 13);
			this.label3.TabIndex = 4;
			this.label3.Text = "Password";
			// 
			// txtLogin
			// 
			this.txtLogin.Location = new System.Drawing.Point(76, 71);
			this.txtLogin.Name = "txtLogin";
			this.txtLogin.Size = new System.Drawing.Size(100, 20);
			this.txtLogin.TabIndex = 1;
			this.txtLogin.Text = "Ostrikov";
			this.txtLogin.TextChanged += new System.EventHandler(this.txtLogin_TextChanged);
			// 
			// txtPassword
			// 
			this.txtPassword.Location = new System.Drawing.Point(76, 94);
			this.txtPassword.Name = "txtPassword";
			this.txtPassword.Size = new System.Drawing.Size(100, 20);
			this.txtPassword.TabIndex = 2;
			this.txtPassword.Text = "QAZwsx111";
			this.txtPassword.UseSystemPasswordChar = true;
			this.txtPassword.TextChanged += new System.EventHandler(this.txtPassword_TextChanged);
			// 
			// btnStartSession
			// 
			this.btnStartSession.Location = new System.Drawing.Point(15, 127);
			this.btnStartSession.Name = "btnStartSession";
			this.btnStartSession.Size = new System.Drawing.Size(87, 23);
			this.btnStartSession.TabIndex = 3;
			this.btnStartSession.Text = "Start Session";
			this.btnStartSession.UseVisualStyleBackColor = true;
			this.btnStartSession.Click += new System.EventHandler(this.btnStartSession_Click);
			// 
			// btnContinuSession
			// 
			this.btnContinuSession.Location = new System.Drawing.Point(15, 156);
			this.btnContinuSession.Name = "btnContinuSession";
			this.btnContinuSession.Size = new System.Drawing.Size(104, 23);
			this.btnContinuSession.TabIndex = 6;
			this.btnContinuSession.Text = "Continue Session";
			this.btnContinuSession.UseVisualStyleBackColor = true;
			this.btnContinuSession.Click += new System.EventHandler(this.btnContinuSession_Click);
			// 
			// txtContinueSessionRes
			// 
			this.txtContinueSessionRes.Location = new System.Drawing.Point(136, 156);
			this.txtContinueSessionRes.Name = "txtContinueSessionRes";
			this.txtContinueSessionRes.Size = new System.Drawing.Size(65, 20);
			this.txtContinueSessionRes.TabIndex = 7;
			this.txtContinueSessionRes.TextChanged += new System.EventHandler(this.txtContinueSessionRes_TextChanged);
			// 
			// btnGetRootOrgUnit
			// 
			this.btnGetRootOrgUnit.Location = new System.Drawing.Point(15, 191);
			this.btnGetRootOrgUnit.Name = "btnGetRootOrgUnit";
			this.btnGetRootOrgUnit.Size = new System.Drawing.Size(104, 23);
			this.btnGetRootOrgUnit.TabIndex = 12;
			this.btnGetRootOrgUnit.Text = "Get Root Org Unit";
			this.btnGetRootOrgUnit.UseVisualStyleBackColor = true;
			this.btnGetRootOrgUnit.Click += new System.EventHandler(this.btnGetRootOrgUnit_Click);
			// 
			// txtCommonSessionID
			// 
			this.txtCommonSessionID.Location = new System.Drawing.Point(128, 14);
			this.txtCommonSessionID.Name = "txtCommonSessionID";
			this.txtCommonSessionID.Size = new System.Drawing.Size(208, 20);
			this.txtCommonSessionID.TabIndex = 13;
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(12, 18);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(99, 13);
			this.label7.TabIndex = 14;
			this.label7.Text = "Common SessionID";
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(355, 18);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(54, 13);
			this.label8.TabIndex = 15;
			this.label8.Text = "OrgUnitID";
			// 
			// txtOrgUnitID
			// 
			this.txtOrgUnitID.Location = new System.Drawing.Point(415, 14);
			this.txtOrgUnitID.Name = "txtOrgUnitID";
			this.txtOrgUnitID.Size = new System.Drawing.Size(132, 20);
			this.txtOrgUnitID.TabIndex = 16;
			// 
			// btnGetOrgUnit
			// 
			this.btnGetOrgUnit.Location = new System.Drawing.Point(15, 244);
			this.btnGetOrgUnit.Name = "btnGetOrgUnit";
			this.btnGetOrgUnit.Size = new System.Drawing.Size(75, 23);
			this.btnGetOrgUnit.TabIndex = 17;
			this.btnGetOrgUnit.Text = "Get Org Unit";
			this.btnGetOrgUnit.UseVisualStyleBackColor = true;
			this.btnGetOrgUnit.Click += new System.EventHandler(this.btnGetOrgUnit_Click);
			// 
			// btnGetOrgUnitSubs
			// 
			this.btnGetOrgUnitSubs.Location = new System.Drawing.Point(97, 244);
			this.btnGetOrgUnitSubs.Name = "btnGetOrgUnitSubs";
			this.btnGetOrgUnitSubs.Size = new System.Drawing.Size(104, 23);
			this.btnGetOrgUnitSubs.TabIndex = 18;
			this.btnGetOrgUnitSubs.Text = "Get Org Unit Subs";
			this.btnGetOrgUnitSubs.UseVisualStyleBackColor = true;
			this.btnGetOrgUnitSubs.Click += new System.EventHandler(this.btnGetOrgUnitSubs_Click);
			// 
			// txtPersID
			// 
			this.txtPersID.Location = new System.Drawing.Point(230, 43);
			this.txtPersID.Name = "txtPersID";
			this.txtPersID.Size = new System.Drawing.Size(149, 20);
			this.txtPersID.TabIndex = 19;
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.Location = new System.Drawing.Point(182, 47);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(42, 13);
			this.label9.TabIndex = 20;
			this.label9.Text = "Pesr ID";
			// 
			// pgObjectData
			// 
			this.pgObjectData.Location = new System.Drawing.Point(403, 74);
			this.pgObjectData.Name = "pgObjectData";
			this.pgObjectData.Size = new System.Drawing.Size(382, 222);
			this.pgObjectData.TabIndex = 21;
			// 
			// btnGetPerson
			// 
			this.btnGetPerson.Location = new System.Drawing.Point(15, 321);
			this.btnGetPerson.Name = "btnGetPerson";
			this.btnGetPerson.Size = new System.Drawing.Size(75, 23);
			this.btnGetPerson.TabIndex = 22;
			this.btnGetPerson.Text = "Get Person";
			this.btnGetPerson.UseVisualStyleBackColor = true;
			this.btnGetPerson.Click += new System.EventHandler(this.btnGetPerson_Click);
			// 
			// pbPersPhoto
			// 
			this.pbPersPhoto.Location = new System.Drawing.Point(193, 74);
			this.pbPersPhoto.Name = "pbPersPhoto";
			this.pbPersPhoto.Size = new System.Drawing.Size(201, 67);
			this.pbPersPhoto.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.pbPersPhoto.TabIndex = 23;
			this.pbPersPhoto.TabStop = false;
			this.pbPersPhoto.DoubleClick += new System.EventHandler(this.pbPersPhoto_DoubleClick);
			// 
			// btnGetAccList
			// 
			this.btnGetAccList.Location = new System.Drawing.Point(15, 283);
			this.btnGetAccList.Name = "btnGetAccList";
			this.btnGetAccList.Size = new System.Drawing.Size(75, 23);
			this.btnGetAccList.TabIndex = 24;
			this.btnGetAccList.Text = "Get Acc List";
			this.btnGetAccList.UseVisualStyleBackColor = true;
			this.btnGetAccList.Click += new System.EventHandler(this.btnGetAccList_Click);
			// 
			// btnGetPersonIdentifiers
			// 
			this.btnGetPersonIdentifiers.Location = new System.Drawing.Point(96, 321);
			this.btnGetPersonIdentifiers.Name = "btnGetPersonIdentifiers";
			this.btnGetPersonIdentifiers.Size = new System.Drawing.Size(118, 23);
			this.btnGetPersonIdentifiers.TabIndex = 25;
			this.btnGetPersonIdentifiers.Text = "Get Person Identifiers";
			this.btnGetPersonIdentifiers.UseVisualStyleBackColor = true;
			this.btnGetPersonIdentifiers.Click += new System.EventHandler(this.btnGetPersonIdentifiers_Click);
			// 
			// btnGetOrgHierarhy
			// 
			this.btnGetOrgHierarhy.Location = new System.Drawing.Point(218, 244);
			this.btnGetOrgHierarhy.Name = "btnGetOrgHierarhy";
			this.btnGetOrgHierarhy.Size = new System.Drawing.Size(94, 23);
			this.btnGetOrgHierarhy.TabIndex = 26;
			this.btnGetOrgHierarhy.Text = "Get Org Hierarhy";
			this.btnGetOrgHierarhy.UseVisualStyleBackColor = true;
			this.btnGetOrgHierarhy.Click += new System.EventHandler(this.btnGetOrgHierarhy_Click);
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(385, 47);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(76, 13);
			this.label4.TabIndex = 27;
			this.label4.Text = "Edit SessionID";
			// 
			// txtEditSessionID
			// 
			this.txtEditSessionID.Location = new System.Drawing.Point(467, 43);
			this.txtEditSessionID.Name = "txtEditSessionID";
			this.txtEditSessionID.Size = new System.Drawing.Size(137, 20);
			this.txtEditSessionID.TabIndex = 28;
			// 
			// btnEditOrgUnit
			// 
			this.btnEditOrgUnit.Location = new System.Drawing.Point(108, 355);
			this.btnEditOrgUnit.Name = "btnEditOrgUnit";
			this.btnEditOrgUnit.Size = new System.Drawing.Size(75, 23);
			this.btnEditOrgUnit.TabIndex = 29;
			this.btnEditOrgUnit.Text = "Edit OrgUnit";
			this.btnEditOrgUnit.UseVisualStyleBackColor = true;
			this.btnEditOrgUnit.Click += new System.EventHandler(this.btnEditOrgUnit_Click);
			// 
			// btnSaveOrgUnit
			// 
			this.btnSaveOrgUnit.Location = new System.Drawing.Point(190, 355);
			this.btnSaveOrgUnit.Name = "btnSaveOrgUnit";
			this.btnSaveOrgUnit.Size = new System.Drawing.Size(87, 23);
			this.btnSaveOrgUnit.TabIndex = 30;
			this.btnSaveOrgUnit.Text = "Save OrgUnit";
			this.btnSaveOrgUnit.UseVisualStyleBackColor = true;
			this.btnSaveOrgUnit.Click += new System.EventHandler(this.btnSaveOrgUnit_Click);
			// 
			// btnDeleteOrgUnit
			// 
			this.btnDeleteOrgUnit.Location = new System.Drawing.Point(354, 355);
			this.btnDeleteOrgUnit.Name = "btnDeleteOrgUnit";
			this.btnDeleteOrgUnit.Size = new System.Drawing.Size(88, 23);
			this.btnDeleteOrgUnit.TabIndex = 31;
			this.btnDeleteOrgUnit.Text = "Delete OrgUnit";
			this.btnDeleteOrgUnit.UseVisualStyleBackColor = true;
			this.btnDeleteOrgUnit.Click += new System.EventHandler(this.btnDeleteOrgUnit_Click);
			// 
			// btnSavePerson
			// 
			this.btnSavePerson.Location = new System.Drawing.Point(189, 383);
			this.btnSavePerson.Name = "btnSavePerson";
			this.btnSavePerson.Size = new System.Drawing.Size(80, 23);
			this.btnSavePerson.TabIndex = 33;
			this.btnSavePerson.Text = "Save Person";
			this.btnSavePerson.UseVisualStyleBackColor = true;
			this.btnSavePerson.Click += new System.EventHandler(this.btnSavePerson_Click);
			// 
			// btnSavePersonShort
			// 
			this.btnSavePersonShort.Location = new System.Drawing.Point(275, 383);
			this.btnSavePersonShort.Name = "btnSavePersonShort";
			this.btnSavePersonShort.Size = new System.Drawing.Size(104, 23);
			this.btnSavePersonShort.TabIndex = 34;
			this.btnSavePersonShort.Text = "Save Person Short";
			this.btnSavePersonShort.UseVisualStyleBackColor = true;
			this.btnSavePersonShort.Click += new System.EventHandler(this.btnSavePersonShort_Click);
			// 
			// btnSetPhoto
			// 
			this.btnSetPhoto.Location = new System.Drawing.Point(385, 383);
			this.btnSetPhoto.Name = "btnSetPhoto";
			this.btnSetPhoto.Size = new System.Drawing.Size(75, 23);
			this.btnSetPhoto.TabIndex = 35;
			this.btnSetPhoto.Text = "Set Photo";
			this.btnSetPhoto.UseVisualStyleBackColor = true;
			this.btnSetPhoto.Click += new System.EventHandler(this.btnSetPhoto_Click);
			// 
			// btnDeletePerson
			// 
			this.btnDeletePerson.Location = new System.Drawing.Point(547, 383);
			this.btnDeletePerson.Name = "btnDeletePerson";
			this.btnDeletePerson.Size = new System.Drawing.Size(89, 23);
			this.btnDeletePerson.TabIndex = 36;
			this.btnDeletePerson.Text = "Delete Person";
			this.btnDeletePerson.UseVisualStyleBackColor = true;
			this.btnDeletePerson.Click += new System.EventHandler(this.btnDeletePerson_Click);
			// 
			// btnSetOrgUnit
			// 
			this.btnSetOrgUnit.Location = new System.Drawing.Point(466, 383);
			this.btnSetOrgUnit.Name = "btnSetOrgUnit";
			this.btnSetOrgUnit.Size = new System.Drawing.Size(75, 23);
			this.btnSetOrgUnit.TabIndex = 37;
			this.btnSetOrgUnit.Text = "Set OrgUnit";
			this.btnSetOrgUnit.UseVisualStyleBackColor = true;
			this.btnSetOrgUnit.Click += new System.EventHandler(this.btnSetOrgUnit_Click);
			// 
			// btnCreateOrgUnit
			// 
			this.btnCreateOrgUnit.Location = new System.Drawing.Point(15, 355);
			this.btnCreateOrgUnit.Name = "btnCreateOrgUnit";
			this.btnCreateOrgUnit.Size = new System.Drawing.Size(87, 23);
			this.btnCreateOrgUnit.TabIndex = 38;
			this.btnCreateOrgUnit.Text = "Create OrgUnit";
			this.btnCreateOrgUnit.UseVisualStyleBackColor = true;
			this.btnCreateOrgUnit.Click += new System.EventHandler(this.btnCreateOrgUnit_Click);
			// 
			// btnCreatePerson
			// 
			this.btnCreatePerson.Location = new System.Drawing.Point(15, 383);
			this.btnCreatePerson.Name = "btnCreatePerson";
			this.btnCreatePerson.Size = new System.Drawing.Size(87, 23);
			this.btnCreatePerson.TabIndex = 39;
			this.btnCreatePerson.Text = "Create Person";
			this.btnCreatePerson.UseVisualStyleBackColor = true;
			this.btnCreatePerson.Click += new System.EventHandler(this.btnCreatePerson_Click);
			// 
			// txtRes
			// 
			this.txtRes.Location = new System.Drawing.Point(403, 321);
			this.txtRes.Name = "txtRes";
			this.txtRes.Size = new System.Drawing.Size(39, 20);
			this.txtRes.TabIndex = 40;
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(376, 324);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(21, 13);
			this.label5.TabIndex = 41;
			this.label5.Text = "res";
			// 
			// txtErrMess
			// 
			this.txtErrMess.Location = new System.Drawing.Point(448, 321);
			this.txtErrMess.Multiline = true;
			this.txtErrMess.Name = "txtErrMess";
			this.txtErrMess.Size = new System.Drawing.Size(337, 56);
			this.txtErrMess.TabIndex = 42;
			// 
			// btnSaveOrgUnitShort
			// 
			this.btnSaveOrgUnitShort.Location = new System.Drawing.Point(283, 355);
			this.btnSaveOrgUnitShort.Name = "btnSaveOrgUnitShort";
			this.btnSaveOrgUnitShort.Size = new System.Drawing.Size(65, 23);
			this.btnSaveOrgUnitShort.TabIndex = 43;
			this.btnSaveOrgUnitShort.Text = "Short";
			this.btnSaveOrgUnitShort.UseVisualStyleBackColor = true;
			this.btnSaveOrgUnitShort.Click += new System.EventHandler(this.btnSaveOrgUnitShort_Click);
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(553, 18);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(42, 13);
			this.label6.TabIndex = 44;
			this.label6.Text = "IdentID";
			// 
			// txtIdentiID
			// 
			this.txtIdentiID.Location = new System.Drawing.Point(606, 14);
			this.txtIdentiID.Name = "txtIdentiID";
			this.txtIdentiID.Size = new System.Drawing.Size(179, 20);
			this.txtIdentiID.TabIndex = 45;
			// 
			// btnIdentifBeginEditSession
			// 
			this.btnIdentifBeginEditSession.Location = new System.Drawing.Point(15, 412);
			this.btnIdentifBeginEditSession.Name = "btnIdentifBeginEditSession";
			this.btnIdentifBeginEditSession.Size = new System.Drawing.Size(104, 23);
			this.btnIdentifBeginEditSession.TabIndex = 46;
			this.btnIdentifBeginEditSession.Text = "Begin Edit Person";
			this.btnIdentifBeginEditSession.UseVisualStyleBackColor = true;
			this.btnIdentifBeginEditSession.Click += new System.EventHandler(this.btnIdentifBeginEditSession_Click);
			// 
			// btnEndEditIdent
			// 
			this.btnEndEditIdent.Location = new System.Drawing.Point(125, 412);
			this.btnEndEditIdent.Name = "btnEndEditIdent";
			this.btnEndEditIdent.Size = new System.Drawing.Size(97, 23);
			this.btnEndEditIdent.TabIndex = 47;
			this.btnEndEditIdent.Text = "End Edit Person";
			this.btnEndEditIdent.UseVisualStyleBackColor = true;
			this.btnEndEditIdent.Click += new System.EventHandler(this.btnEndEditIdent_Click);
			// 
			// btnCreateIdent
			// 
			this.btnCreateIdent.Location = new System.Drawing.Point(401, 412);
			this.btnCreateIdent.Name = "btnCreateIdent";
			this.btnCreateIdent.Size = new System.Drawing.Size(75, 23);
			this.btnCreateIdent.TabIndex = 48;
			this.btnCreateIdent.Text = "Create Ident";
			this.btnCreateIdent.UseVisualStyleBackColor = true;
			this.btnCreateIdent.Click += new System.EventHandler(this.btnCreateIdent_Click);
			// 
			// btnSaveIdent
			// 
			this.btnSaveIdent.Location = new System.Drawing.Point(482, 412);
			this.btnSaveIdent.Name = "btnSaveIdent";
			this.btnSaveIdent.Size = new System.Drawing.Size(75, 23);
			this.btnSaveIdent.TabIndex = 49;
			this.btnSaveIdent.Text = "Save Ident";
			this.btnSaveIdent.UseVisualStyleBackColor = true;
			this.btnSaveIdent.Click += new System.EventHandler(this.btnSaveIdent_Click);
			// 
			// btnDeleteIdent
			// 
			this.btnDeleteIdent.Location = new System.Drawing.Point(563, 412);
			this.btnDeleteIdent.Name = "btnDeleteIdent";
			this.btnDeleteIdent.Size = new System.Drawing.Size(75, 23);
			this.btnDeleteIdent.TabIndex = 50;
			this.btnDeleteIdent.Text = "Delete Ident";
			this.btnDeleteIdent.UseVisualStyleBackColor = true;
			this.btnDeleteIdent.Click += new System.EventHandler(this.btnDeleteIdent_Click);
			// 
			// btnGetIdentifier
			// 
			this.btnGetIdentifier.Location = new System.Drawing.Point(319, 412);
			this.btnGetIdentifier.Name = "btnGetIdentifier";
			this.btnGetIdentifier.Size = new System.Drawing.Size(75, 23);
			this.btnGetIdentifier.TabIndex = 51;
			this.btnGetIdentifier.Text = "Get Ident";
			this.btnGetIdentifier.UseVisualStyleBackColor = true;
			this.btnGetIdentifier.Click += new System.EventHandler(this.btnGetIdentifier_Click);
			// 
			// chkIsTemp
			// 
			this.chkIsTemp.AutoSize = true;
			this.chkIsTemp.Location = new System.Drawing.Point(314, 154);
			this.chkIsTemp.Name = "chkIsTemp";
			this.chkIsTemp.Size = new System.Drawing.Size(64, 17);
			this.chkIsTemp.TabIndex = 52;
			this.chkIsTemp.Text = "Is Temp";
			this.chkIsTemp.UseVisualStyleBackColor = true;
			// 
			// dtpFrom
			// 
			this.dtpFrom.Location = new System.Drawing.Point(242, 183);
			this.dtpFrom.Name = "dtpFrom";
			this.dtpFrom.Size = new System.Drawing.Size(155, 20);
			this.dtpFrom.TabIndex = 53;
			this.dtpFrom.ValueChanged += new System.EventHandler(this.dtpFrom_ValueChanged);
			// 
			// dtpTo
			// 
			this.dtpTo.Location = new System.Drawing.Point(242, 218);
			this.dtpTo.Name = "dtpTo";
			this.dtpTo.Size = new System.Drawing.Size(155, 20);
			this.dtpTo.TabIndex = 54;
			// 
			// label10
			// 
			this.label10.AutoSize = true;
			this.label10.Location = new System.Drawing.Point(610, 47);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(43, 13);
			this.label10.TabIndex = 55;
			this.label10.Text = "TerraID";
			// 
			// txtTerraID
			// 
			this.txtTerraID.Location = new System.Drawing.Point(659, 42);
			this.txtTerraID.Name = "txtTerraID";
			this.txtTerraID.Size = new System.Drawing.Size(129, 20);
			this.txtTerraID.TabIndex = 56;
			// 
			// btnGetRootTerritory
			// 
			this.btnGetRootTerritory.Location = new System.Drawing.Point(15, 442);
			this.btnGetRootTerritory.Name = "btnGetRootTerritory";
			this.btnGetRootTerritory.Size = new System.Drawing.Size(87, 23);
			this.btnGetRootTerritory.TabIndex = 57;
			this.btnGetRootTerritory.Text = "GetRootTerra";
			this.btnGetRootTerritory.UseVisualStyleBackColor = true;
			this.btnGetRootTerritory.Click += new System.EventHandler(this.btnGetRootTerritory_Click);
			// 
			// btnGetTerraHierarhy
			// 
			this.btnGetTerraHierarhy.Location = new System.Drawing.Point(108, 441);
			this.btnGetTerraHierarhy.Name = "btnGetTerraHierarhy";
			this.btnGetTerraHierarhy.Size = new System.Drawing.Size(106, 23);
			this.btnGetTerraHierarhy.TabIndex = 58;
			this.btnGetTerraHierarhy.Text = "Get Terra Hierarhy";
			this.btnGetTerraHierarhy.UseVisualStyleBackColor = true;
			this.btnGetTerraHierarhy.Click += new System.EventHandler(this.btnGetTerraHierarhy_Click);
			// 
			// btnGetEvents
			// 
			this.btnGetEvents.Location = new System.Drawing.Point(15, 472);
			this.btnGetEvents.Name = "btnGetEvents";
			this.btnGetEvents.Size = new System.Drawing.Size(75, 23);
			this.btnGetEvents.TabIndex = 59;
			this.btnGetEvents.Text = "Get Events";
			this.btnGetEvents.UseVisualStyleBackColor = true;
			this.btnGetEvents.Click += new System.EventHandler(this.btnGetEvents_Click);
			// 
			// btnGetFullHierarhy
			// 
			this.btnGetFullHierarhy.Location = new System.Drawing.Point(218, 283);
			this.btnGetFullHierarhy.Name = "btnGetFullHierarhy";
			this.btnGetFullHierarhy.Size = new System.Drawing.Size(94, 23);
			this.btnGetFullHierarhy.TabIndex = 60;
			this.btnGetFullHierarhy.Text = "Get Full OrgUnit Hierarhy";
			this.btnGetFullHierarhy.UseVisualStyleBackColor = true;
			this.btnGetFullHierarhy.Click += new System.EventHandler(this.btnGetFullHierarhy_Click);
			// 
			// button1
			// 
			this.button1.BackColor = System.Drawing.Color.Chartreuse;
			this.button1.Location = new System.Drawing.Point(686, 383);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(125, 23);
			this.button1.TabIndex = 61;
			this.button1.Text = "VisitorRequest";
			this.button1.UseVisualStyleBackColor = false;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(866, 541);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.btnGetFullHierarhy);
			this.Controls.Add(this.btnGetEvents);
			this.Controls.Add(this.btnGetTerraHierarhy);
			this.Controls.Add(this.btnGetRootTerritory);
			this.Controls.Add(this.txtTerraID);
			this.Controls.Add(this.label10);
			this.Controls.Add(this.dtpTo);
			this.Controls.Add(this.dtpFrom);
			this.Controls.Add(this.chkIsTemp);
			this.Controls.Add(this.btnGetIdentifier);
			this.Controls.Add(this.btnDeleteIdent);
			this.Controls.Add(this.btnSaveIdent);
			this.Controls.Add(this.btnCreateIdent);
			this.Controls.Add(this.btnEndEditIdent);
			this.Controls.Add(this.btnIdentifBeginEditSession);
			this.Controls.Add(this.txtIdentiID);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.btnSaveOrgUnitShort);
			this.Controls.Add(this.txtErrMess);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.txtRes);
			this.Controls.Add(this.btnCreatePerson);
			this.Controls.Add(this.btnCreateOrgUnit);
			this.Controls.Add(this.btnSetOrgUnit);
			this.Controls.Add(this.btnDeletePerson);
			this.Controls.Add(this.btnSetPhoto);
			this.Controls.Add(this.btnSavePersonShort);
			this.Controls.Add(this.btnSavePerson);
			this.Controls.Add(this.btnDeleteOrgUnit);
			this.Controls.Add(this.btnSaveOrgUnit);
			this.Controls.Add(this.btnEditOrgUnit);
			this.Controls.Add(this.txtEditSessionID);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.btnGetOrgHierarhy);
			this.Controls.Add(this.btnGetPersonIdentifiers);
			this.Controls.Add(this.btnGetAccList);
			this.Controls.Add(this.pbPersPhoto);
			this.Controls.Add(this.btnGetPerson);
			this.Controls.Add(this.pgObjectData);
			this.Controls.Add(this.label9);
			this.Controls.Add(this.txtPersID);
			this.Controls.Add(this.btnGetOrgUnitSubs);
			this.Controls.Add(this.btnGetOrgUnit);
			this.Controls.Add(this.txtOrgUnitID);
			this.Controls.Add(this.label8);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.txtCommonSessionID);
			this.Controls.Add(this.btnGetRootOrgUnit);
			this.Controls.Add(this.txtContinueSessionRes);
			this.Controls.Add(this.btnContinuSession);
			this.Controls.Add(this.btnStartSession);
			this.Controls.Add(this.txtPassword);
			this.Controls.Add(this.txtLogin);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.txtDomain);
			this.Name = "Form1";
			this.Text = "Form1";
			this.Load += new System.EventHandler(this.Form1_Load);
			((System.ComponentModel.ISupportInitialize)(this.pbPersPhoto)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtDomain;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtLogin;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Button btnStartSession;
        private System.Windows.Forms.Button btnContinuSession;
        private System.Windows.Forms.TextBox txtContinueSessionRes;
        private System.Windows.Forms.Button btnGetRootOrgUnit;
        private System.Windows.Forms.TextBox txtCommonSessionID;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtOrgUnitID;
        private System.Windows.Forms.Button btnGetOrgUnit;
        private System.Windows.Forms.Button btnGetOrgUnitSubs;
        private System.Windows.Forms.TextBox txtPersID;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.PropertyGrid pgObjectData;
        private System.Windows.Forms.Button btnGetPerson;
        private System.Windows.Forms.PictureBox pbPersPhoto;
        private System.Windows.Forms.Button btnGetAccList;
        private System.Windows.Forms.Button btnGetPersonIdentifiers;
        private System.Windows.Forms.Button btnGetOrgHierarhy;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtEditSessionID;
        private System.Windows.Forms.Button btnEditOrgUnit;
        private System.Windows.Forms.Button btnSaveOrgUnit;
        private System.Windows.Forms.Button btnDeleteOrgUnit;
        private System.Windows.Forms.Button btnSavePerson;
        private System.Windows.Forms.Button btnSavePersonShort;
        private System.Windows.Forms.Button btnSetPhoto;
        private System.Windows.Forms.Button btnDeletePerson;
        private System.Windows.Forms.Button btnSetOrgUnit;
        private System.Windows.Forms.Button btnCreateOrgUnit;
        private System.Windows.Forms.Button btnCreatePerson;
        private System.Windows.Forms.TextBox txtRes;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtErrMess;
        private System.Windows.Forms.Button btnSaveOrgUnitShort;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtIdentiID;
        private System.Windows.Forms.Button btnIdentifBeginEditSession;
        private System.Windows.Forms.Button btnEndEditIdent;
        private System.Windows.Forms.Button btnCreateIdent;
        private System.Windows.Forms.Button btnSaveIdent;
        private System.Windows.Forms.Button btnDeleteIdent;
        private System.Windows.Forms.Button btnGetIdentifier;
        private System.Windows.Forms.CheckBox chkIsTemp;
        private System.Windows.Forms.DateTimePicker dtpFrom;
        private System.Windows.Forms.DateTimePicker dtpTo;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtTerraID;
        private System.Windows.Forms.Button btnGetRootTerritory;
        private System.Windows.Forms.Button btnGetTerraHierarhy;
        private System.Windows.Forms.Button btnGetEvents;
        private System.Windows.Forms.Button btnGetFullHierarhy;

		private void Form1_Load(object sender, EventArgs e)
		{

		}

		private void txtLogin_TextChanged(object sender, EventArgs e)
		{

		}

		private void txtPassword_TextChanged(object sender, EventArgs e)
		{

		}
		//============================Остриков============================================
		//Заявка на пропуск
		private void button1_Click(object sender, EventArgs e)
		{
			ClearResult();
			IntegrationService integrService = new IntegrationService();
			string domain = "SYSTEM";
			string userName = "Ostrikov";
			string password = "QAZwsx111";
			SessionResult res = integrService.OpenSession(domain, userName, password);
			Guid SessionID = new Guid(txtCommonSessionID.Text);
			

			//1. Создаем физлицо будущего посетителя
			string mySessionID = res.Value.SessionID.ToString();
			string myRootOrgUnitID = res.Value.RootOrgUnitID.ToString();
			//txtTerraID.Text = res.Value.RootTerritoryID.ToString();

			Person myGuest = new Person();
			myGuest.ID = Guid.Empty;
			myGuest.LAST_NAME = "Трамп";
			myGuest.FIRST_NAME = "Дональд";
			myGuest.MIDDLE_NAME = "Обамович";
			myGuest.TAB_NUM = "guest-0001";
			myGuest.ORG_ID = Guid.Parse(myRootOrgUnitID);

			//2. Создаем посетителя из физлица (Голубая иконка)
			var res2 = integrService.CreateVisitor(SessionID, myGuest );


			//3. Создаем заявку на проход
			VisitorRequest myVisitor = new VisitorRequest();

			myVisitor.NUMBER = 777;
			myVisitor.PERSON_INFO ="Очень важный пассажир";
			myVisitor.PURPOSE = "Деловая"; //Цель визита
			//myVisitor.STATUS =;
			//myVisitor.ADMIT_START =;
			//myVisitor.ADMIT_END =;
			myVisitor.DATE = new DateTime(2019, 10, 21);

			//Отправляем запрос на Parsec Заявку
			var res3 = integrService.CreateVisitorRequest(SessionID,myVisitor);
		   
			
		}

		private void dtpFrom_ValueChanged(object sender, EventArgs e)
		{

		}

		private void txtContinueSessionRes_TextChanged(object sender, EventArgs e)
		{

		}
	}
}
