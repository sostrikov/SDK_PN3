unit uSelectReportParam;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, ComCtrls, StdCtrls, ExtCtrls, Buttons;

type
  TfrmSelectReportParam = class(TForm)
    Panel1: TPanel;
    Label4: TLabel;
    Label5: TLabel;
    btnOK: TButton;
    btnCancel: TButton;
    dtpFrom: TDateTimePicker;
    dtpTo: TDateTimePicker;
    Label1: TLabel;
    edtOrg: TEdit;
    btnBrowseOrg: TSpeedButton;
    Label2: TLabel;
    edtTerritory: TEdit;
    btnBrowseTerritory: TSpeedButton;
    dtpFromTime: TDateTimePicker;
    dtpToTime: TDateTimePicker;
    procedure FormCreate(Sender: TObject);
    procedure btnBrowseOrgClick(Sender: TObject);
    procedure btnBrowseTerritoryClick(Sender: TObject);
  private
    { Private declarations }
  public
    { Public declarations }
    SelectedOrgID: string;
    SelectedTerritoryID: string;
    FromDT: TDateTime;
    ToDT: TDateTime;
    procedure FormatDTP;
  end;

var
  frmSelectReportParam: TfrmSelectReportParam;

implementation

uses uSelectOrg, uMain, uSelectTerritory;

{$R *.dfm}

procedure TfrmSelectReportParam.FormatDTP;
begin
  FromDT:= dtpFrom.DateTime;
  ToDT:= dtpTo.DateTime;
  ReplaceTime(toDT, dtpToTime.Time);
  ReplaceTime(fromDT, dtpFromTime.Time);
end;

procedure TfrmSelectReportParam.btnBrowseTerritoryClick(Sender: TObject);
begin
  frmSelectTerritory:= TfrmSelectTerritory.Create(self);
  try
    frmMain.FillTreeViewTerritory(frmSelectTerritory.tvTerritory);
    if frmSelectTerritory.ShowModal = mrOk then
    begin
      edtTerritory.Text:= frmSelectTerritory.SelectedTerritoryNAME;
      SelectedTerritoryID:= frmSelectTerritory.SelectedTerritoryID;
    end;
  finally
    frmSelectTerritory.Free;
  end;
end;

procedure TfrmSelectReportParam.FormCreate(Sender: TObject);
begin
  dtpFrom.Date:= now;
  dtpTo.Date:= now;
  dtpFromTime.Time:= 0;
  dtpToTime.Time:= now;
end;

procedure TfrmSelectReportParam.btnBrowseOrgClick(Sender: TObject);
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

end.
