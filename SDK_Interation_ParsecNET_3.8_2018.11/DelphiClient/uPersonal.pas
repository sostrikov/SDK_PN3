unit uPersonal;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, ExtCtrls, StdCtrls, ComCtrls, Buttons, IntegrationService, ExtDlgs;

type
  TfrmPersonal = class(TForm)
    btnCancel: TButton;
    btnOK: TButton;
    Panel1: TPanel;
    Label1: TLabel;
    edtFirstName: TEdit;
    Label2: TLabel;
    edtLastName: TEdit;
    Label3: TLabel;
    edtMiddleName: TEdit;
    Label4: TLabel;
    edtTabNum: TEdit;
    Image1: TImage;
    Bevel1: TBevel;
    Label5: TLabel;
    edtOrg: TEdit;
    SpeedButton1: TSpeedButton;
    SpeedButton2: TSpeedButton;
    OpenPictureDialog1: TOpenPictureDialog;
    Label6: TLabel;
    edtKey: TEdit;
    Label7: TLabel;
    cbAccessGroup: TComboBox;
    procedure SpeedButton1Click(Sender: TObject);
    procedure SpeedButton2Click(Sender: TObject);
  private
    { Private declarations }
  public
    { Public declarations }
    SelectedOrgID: string;
  end;

var
  frmPersonal: TfrmPersonal;

implementation

{$R *.dfm}

uses uMain, uSelectOrg;

procedure TfrmPersonal.SpeedButton1Click(Sender: TObject);
begin
  frmSelectOrg:= TfrmSelectOrg.Create(self);
  try
    frmMain.FillTreeViewOrg(frmSelectOrg.tvOrg);
    if frmSelectOrg.ShowModal = mrOk then
    begin
      edtOrg.Text:= frmSelectOrg.SelectedOrgNAME;
      SelectedOrgID:= frmSelectOrg.SelectedOrgID;
    end;
  finally
    frmSelectOrg.Free;
  end;
end;

procedure TfrmPersonal.SpeedButton2Click(Sender: TObject);
begin
  if OpenPictureDialog1.Execute then
    Image1.Picture.LoadFromFile(OpenPictureDialog1.FileName);
end;

end.
