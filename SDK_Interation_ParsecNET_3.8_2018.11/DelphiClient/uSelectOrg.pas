unit uSelectOrg;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, ImgList, ComCtrls, IntegrationService;

type
  TfrmSelectOrg = class(TForm)
    tvOrg: TTreeView;
    ImageList1: TImageList;
    procedure tvOrgDblClick(Sender: TObject);
  private
    { Private declarations }
  public
    { Public declarations }
    SelectedOrgName: string;
    SelectedOrgID: string;
  end;

var
  frmSelectOrg: TfrmSelectOrg;

implementation

{$R *.dfm}

procedure TfrmSelectOrg.tvOrgDblClick(Sender: TObject);
begin
  SelectedOrgName:= OrgUnit(tvOrg.Selected.Data).NAME_;
  SelectedOrgID:= OrgUnit(tvOrg.Selected.Data).ID;
  ModalResult:= mrOK;
end;

end.
