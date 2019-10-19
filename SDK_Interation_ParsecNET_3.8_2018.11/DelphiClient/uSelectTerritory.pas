unit uSelectTerritory;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, ImgList, ComCtrls, IntegrationService;

type
  TfrmSelectTerritory = class(TForm)
    tvTerritory: TTreeView;
    ImageList1: TImageList;
    procedure tvTerritoryDblClick(Sender: TObject);
  private
    { Private declarations }
  public
    { Public declarations }
    SelectedTerritoryName: string;
    SelectedTerritoryID: string;
  end;

var
  frmSelectTerritory: TfrmSelectTerritory;

implementation

{$R *.dfm}

procedure TfrmSelectTerritory.tvTerritoryDblClick(Sender: TObject);
begin
  SelectedTerritoryName:= Territory(tvTerritory.Selected.Data).NAME_;
  SelectedTerritoryID:= Territory(tvTerritory.Selected.Data).ID;
  ModalResult:= mrOK;
end;

end.
