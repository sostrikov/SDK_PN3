unit uMain;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, StdCtrls, IntegrationService, ComCtrls, ImgList, ExtCtrls,
  XSBuiltIns, Types, ActiveX, ToolWin, ActnList, Menus, jpeg, System.Actions;

const
  GUID_EMPTY = '00000000-0000-0000-0000-000000000000';
type
  TfrmMain = class(TForm)
    StatusBar1: TStatusBar;
    CoolBar1: TCoolBar;
    ToolBar1: TToolBar;
    btnClose: TToolButton;
    pcMain: TPageControl;
    TabSheet1: TTabSheet;
    TabSheet2: TTabSheet;
    ImageList1: TImageList;
    ToolButton2: TToolButton;
    btnSettings: TToolButton;
    Panel1: TPanel;
    Splitter1: TSplitter;
    Panel2: TPanel;
    tvOrg: TTreeView;
    Panel3: TPanel;
    Image1: TImage;
    Label1: TLabel;
    Bevel1: TBevel;
    Panel4: TPanel;
    Image2: TImage;
    Label2: TLabel;
    Bevel2: TBevel;
    lvPers: TListView;
    btnFillTreeOrg: TToolButton;
    btnCreatePersonal: TToolButton;
    ActionList1: TActionList;
    actClose: TAction;
    actFillOrg: TAction;
    actAddPers: TAction;
    actEditPers: TAction;
    ToolButton1: TToolButton;
    actProperties: TAction;
    ToolButton3: TToolButton;
    actConnect: TAction;
    ToolButton4: TToolButton;
    actDeletePerson: TAction;
    ToolButton5: TToolButton;
    ToolButton6: TToolButton;
    ToolButton7: TToolButton;
    actStartEventPool: TAction;
    MainMenu1: TMainMenu;
    lvEvent: TListView;
    N1: TMenuItem;
    N2: TMenuItem;
    N3: TMenuItem;
    N4: TMenuItem;
    N5: TMenuItem;
    N6: TMenuItem;
    N7: TMenuItem;
    N8: TMenuItem;
    N9: TMenuItem;
    N10: TMenuItem;
    ToolButton8: TToolButton;
    ToolButton9: TToolButton;
    actAddOrg: TAction;
    ToolButton10: TToolButton;
    actDeleteOrg: TAction;
    N11: TMenuItem;
    N12: TMenuItem;
    N13: TMenuItem;
    pmOrg: TPopupMenu;
    N14: TMenuItem;
    N15: TMenuItem;
    N16: TMenuItem;
    pmPers: TPopupMenu;
    N17: TMenuItem;
    N18: TMenuItem;
    N19: TMenuItem;
    N20: TMenuItem;
    procedure FormDestroy(Sender: TObject);
    procedure tvOrgClick(Sender: TObject);
    procedure actCloseExecute(Sender: TObject);
    procedure actAddPersExecute(Sender: TObject);
    procedure actFillOrgExecute(Sender: TObject);
    procedure actEditPersExecute(Sender: TObject);
    procedure actEditPersUpdate(Sender: TObject);
    procedure actPropertiesExecute(Sender: TObject);
    procedure actConnectExecute(Sender: TObject);
    procedure actFillOrgUpdate(Sender: TObject);
    procedure actDeletePersonExecute(Sender: TObject);
    procedure actStartEventPoolExecute(Sender: TObject);
    procedure FormCreate(Sender: TObject);
    procedure actAddOrgUpdate(Sender: TObject);
    procedure actAddOrgExecute(Sender: TObject);
    procedure actDeleteOrgExecute(Sender: TObject);
  private
    { Private declarations }
    vIntSrv: IntegrationServiceSoap;
    vSession: SessionResult;
    FConnected: boolean;
  public
    { Public declarations }
    procedure FillcbAccessGroup(aComboBox: TComboBox);
    procedure FillTreeViewOrg(aTreeView: TTreeView);
    procedure FillTreeViewTerritory(aTreeView: TTreeView);
    function GetPersonIDByFIO(l,f,m: string): guid;
    function NewGUID: guid;
    function GetOrgNameByID(aOrgID: guid): string;
    procedure HintPanel(Sender: TObject);
  end;

var
  frmMain: TfrmMain;

function GetParsecTempFileName: string;
function PhysicalResolveFileType(AStream: TStream): string;

implementation

uses uPersonal, uProperties, uSelectReportParam;

{$R *.dfm}

procedure TfrmMain.HintPanel(Sender: TObject);
begin
  StatusBar1.Panels[0].Text:= Application.Hint;
end;

function GetParsecTempFileName: string;
var
  TmpPath, TmpFileName: string;
begin
  result:= '';
  SetLength(TmpPath, MAX_PATH + 1);
  if GetTempPath(MAX_PATH, PChar(TmpPath)) <> 0 then
  begin
    SetLength(TmpFileName, MAX_PATH + 1);
    GetTempFileName(PChar(TmpPath), 'PRS', 0, PChar(TmpFileName));
    result:= PChar(TmpFileName);
  end;
end;

function PhysicalResolveFileType(AStream: TStream): string;
var
  p: PChar; 
begin 
  Result := ''; 
  if not Assigned(AStream) then 
    Exit; 
  GetMem(p, 10); 
  try 
    AStream.Position := 0; 
    AStream.Read(p[0], 10); 
    {bitmap format} 
    if (p[0] = #66) and (p[1] = #77) then 
      Result := 'bmp';
    {tiff format} 
    if ((p[0] = #73) and (p[1] = #73) and (p[2] = #42) and (p[3] = #0)) or 
      ((p[0] = #77) and (p[1] = #77) and (p[2] = #42) and (p[3] = #0)) then 
      Result := 'tif';
    {jpg format} 
    if (p[6] = #74) and (p[7] = #70) and (p[8] = #73) and (p[9] = #70) then 
      Result := 'jpg';
    {png format} 
    if (p[0] = #137) and (p[1] = #80) and (p[2] = #78) and (p[3] = #71) and 
      (p[4] = #13) and (p[5] = #10) and (p[6] = #26) and (p[7] = #10) then 
      Result := 'png';
    {dcx format}
    if (p[0] = #177) and (p[1] = #104) and (p[2] = #222) and (p[3] = #58) then 
      Result := 'dcx';
    {pcx format}
    if p[0] = #10 then 
      Result := 'pcx';
    {emf format}
    if (p[0] = #215) and (p[1] = #205) and (p[2] = #198) and (p[3] = #154) then 
      Result := 'emf';
    {emf format} 
    if (p[0] = #1) and (p[1] = #0) and (p[2] = #0) and (p[3] = #0) then
      Result := 'emf'; 
  finally 
    Freemem(p); 
  end; 
end;

function TfrmMain.NewGUID: guid;
var
  ID: TGUID;
begin
  if CoCreateGuid(Id) = s_OK then
    Result:= GUIDToString(Id);
end;

procedure TfrmMain.tvOrgClick(Sender: TObject);
var
  arPers: ArrayOfBaseObject;
  i: integer;
  li: TListItem;
begin
  lvPers.Items.Clear;
  if tvOrg.Selected = nil then
    Exit;

  arPers:= vIntSrv.GetOrgUnitSubItemsHierarhyWithPersons(vSession.Value.SessionID, OrgUnit(tvOrg.Selected.Data).ID);
  for i:= 0 to length(arPers) - 1 do
  begin
    if arPers[i] is BasePerson then
    begin
      li:= lvPers.Items.Add;
      li.Caption:= BasePerson(arPers[i]).LAST_NAME;
      li.SubItems.Add(BasePerson(arPers[i]).FIRST_NAME);
      li.SubItems.Add(BasePerson(arPers[i]).MIDDLE_NAME);
      li.SubItems.Add(BasePerson(arPers[i]).ID);
      li.ImageIndex:= 2;
    end;
  end;
end;

procedure TfrmMain.FillcbAccessGroup(aComboBox: TComboBox);
var
  arGroups: ArrayOfAccessGroup;
  i: integer;
begin
  aComboBox.Items.Clear;
  arGroups:= vIntSrv.GetAccessGroups(vSession.Value.SessionID);
  for i:= 0 to length(arGroups) - 1 do
    aComboBox.Items.AddObject(arGroups[i].NAME_, arGroups[i]);
end;

procedure TfrmMain.FillTreeViewOrg(aTreeView: TTreeView);
var
  aOrgUnit: OrgUnit;
  tn: TTreeNode;
  s: string;
  function GetNodeByID(aTree: TTreeView; aID: string): TTreeNode;
  var
    i: integer;
  begin
    result:= nil;
    for I := 0 to aTree.Items.Count - 1 do
    begin
      if OrgUnit(aTree.Items[i].Data).ID = aID then
      begin
        result:= aTree.Items[i];
        break;
      end;
    end;
  end;
  procedure FillChildren(aTreeView: TTreeView; aOrgUnit: BaseOrgUnit; aParentNode: TTreeNode);
  var
    arOrgUnit: ArrayOfBaseObject;
    i: integer;
    tn: TTreeNode;
  begin
    try
      arOrgUnit:= vIntSrv.GetOrgUnitSubItems(vSession.Value.SessionID, aOrgUnit.ID);
      for i:= 0 to length(arOrgUnit) - 1 do
      begin
        if arOrgUnit[i] is BaseOrgUnit then
        begin
          tn:= aTreeView.Items.AddChild(aParentNode, BaseOrgUnit(arOrgUnit[i]).NAME_);
          tn.ImageIndex:= 52;
          tn.SelectedIndex:= 52;
          tn.Data:= arOrgUnit[i];
          FillChildren(aTreeView, BaseOrgUnit(arOrgUnit[i]), tn);
        end;
      end;
    except
    end;
  end;
begin
  aTreeView.Items.Clear;
  //Добавляем Root
  aOrgUnit:= vIntSrv.GetOrgUnit(vSession.Value.SessionID, vSession.Value.RootOrgUnitID);
  s:= aOrgUnit.NAME_;
  tn:= aTreeView.Items.Add(nil, s);
  tn.ImageIndex:= 33;
  tn.SelectedIndex:= 33;
  tn.Data:= aOrgUnit;
  FillChildren(aTreeView, aOrgUnit, tn);
end;

procedure TfrmMain.FillTreeViewTerritory(aTreeView: TTreeView);
var
  aTerritory: Territory;
  tn: TTreeNode;
  function GetNodeByID(aTree: TTreeView; aID: string): TTreeNode;
  var
    i: integer;
  begin
    result:= nil;
    for I := 0 to aTree.Items.Count - 1 do
    begin
      if Territory(aTree.Items[i].Data).ID = aID then
      begin
        result:= aTree.Items[i];
        break;
      end;
    end;
  end;
  procedure FillChildren(aTreeView: TTreeView; aTerritory: Territory; aParentNode: TTreeNode);
  var
    arTerritory: ArrayOfBaseTerritory;
    i: integer;
    tn: TTreeNode;
  begin
    try
      arTerritory:= vIntSrv.GetTerritorySubItems(vSession.Value.SessionID, aTerritory.ID);
      for i:= 0 to length(arTerritory) - 1 do
      begin
        if arTerritory[i] is BaseTerritory then
        begin
          tn:= aTreeView.Items.AddChild(aParentNode, BaseTerritory(arTerritory[i]).NAME_);
          tn.ImageIndex:= 52;
          tn.SelectedIndex:= 52;
          tn.Data:= arTerritory[i];
          FillChildren(aTreeView, Territory(arTerritory[i]), tn);
        end;
      end;
    except
    end;
  end;
begin
  aTreeView.Items.Clear;
  //Добавляем Root

  aTerritory:= vIntSrv.GetTerritory(vSession.Value.SessionID,  vSession.Value.RootTerritoryID);
  tn:= aTreeView.Items.Add(nil, aTerritory.NAME_);
  tn.ImageIndex:= 33;
  tn.SelectedIndex:= 33;
  tn.Data:= aTerritory;
  FillChildren(aTreeView, aTerritory, tn);
end;

function TfrmMain.GetOrgNameByID(aOrgID: guid): string;
var
  aOrgUnit: OrgUnit;
begin
  aOrgUnit:= vIntSrv.GetOrgUnit(vSession.Value.SessionID, aOrgID);
  result:= aOrgUnit.NAME_;
end;

function TfrmMain.GetPersonIDByFIO(l,f,m: string): guid;
var
  arPers: ArrayOfBaseObject;
  i: integer;
begin
  result:= GUID_EMPTY;
  arPers:= vIntSrv.GetOrgUnitSubItemsHierarhyWithPersons(vSession.Value.SessionID, vSession.Value.RootOrgUnitID);
  for i:= 0 to length(arPers) - 1 do
  begin
    if arPers[i] is BasePerson then
    begin
      if (BasePerson(arPers[i]).LAST_NAME = l)and(BasePerson(arPers[i]).FIRST_NAME = f)and(BasePerson(arPers[i]).MIDDLE_NAME = m) then
      begin
        result:= BasePerson(arPers[i]).ID;
        break;
      end;
    end;
  end;

end;

procedure TfrmMain.FormCreate(Sender: TObject);
begin
  pcMain.ActivePageIndex:= 0;
end;

procedure TfrmMain.FormDestroy(Sender: TObject);
begin
  if FConnected then
    vIntSrv.CloseSession(vSession.Value.SessionID);
end;

procedure TfrmMain.actAddOrgExecute(Sender: TObject);
var
  aOrgUnit: OrgUnit;
  tmp: string;
begin
  if not InputQuery(Caption, 'Введине название организации', tmp) then
    Exit;

  aOrgUnit:= OrgUnit.Create;
  try
    aOrgUnit.PARENT_ID:= OrgUnit(tvOrg.Selected.Data).ID;
    aOrgUnit.NAME_:= tmp;
    aOrgUnit.ID:= GUID_EMPTY;
    vIntSrv.CreateOrgUnit(vSession.Value.SessionID, aOrgUnit);
    actFillOrg.Execute;
  finally
    aOrgUnit.Free;
  end;
end;

procedure TfrmMain.actAddOrgUpdate(Sender: TObject);
begin
  TAction(Sender).Enabled:= not(tvOrg.Selected = nil);
end;

procedure TfrmMain.actAddPersExecute(Sender: TObject);
var
  Res: GuidResult;
  pers: PersonWithPhoto;
  memStream: TMemoryStream;
  buff: TByteDynArray;
  arGroups: ArrayOfAccessGroup;
  i: integer;
  PersCard: Identifier;
begin
  frmPersonal:= TfrmPersonal.Create(self);
  try
    //Заполняем список групп доступа
    FillcbAccessGroup(frmPersonal.cbAccessGroup);

    if frmPersonal.ShowModal = mrOK then
    begin
      pers:= PersonWithPhoto.Create;
      pers.FIRST_NAME:= frmPersonal.edtFirstName.Text;
      pers.LAST_NAME:= frmPersonal.edtLastName.Text;
      pers.MIDDLE_NAME:= frmPersonal.edtMiddleName.Text;
      pers.TAB_NUM:= frmPersonal.edtTABNUM.Text;
      pers.ORG_ID:= frmPersonal.SelectedOrgID;
      pers.ID:= GUID_EMPTY;

      if  Assigned(frmPersonal.Image1.Picture.Graphic) then
      begin
        memStream:= TMemoryStream.Create;
        frmPersonal.Image1.Picture.Graphic.SaveToStream(memStream);
        setLength(buff, memStream.Size);
        memStream.Position:= 0;
        memStream.Read(buff[0], memStream.Size);
        pers.PHOTO:= buff;
        memStream.Free;
      end;

      pers.ID:= vIntSrv.CreatePerson(vSession.Value.SessionID, pers).Value;
      //Добавляем карту
      Res:= vIntSrv.OpenPersonEditingSession(vSession.Value.SessionID, pers.ID);

      PersCard:= Identifier.Create;
      PersCard.CODE:= frmPersonal.edtKey.Text;
      if frmPersonal.cbAccessGroup.ItemIndex >= 0 then
        PersCard.ACCGROUP_ID:= AccessGroup(frmPersonal.cbAccessGroup.Items.Objects[frmPersonal.cbAccessGroup.ItemIndex]).ID;
      vIntSrv.AddPersonIdentifier(res.Value, PersCard);

      vIntSrv.ClosePersonEditingSession(res.Value);

      PersCard.Free;
      pers.Free;
    end;
  finally
    frmPersonal.Free;
  end;
  tvOrgClick(nil);
end;

procedure TfrmMain.actCloseExecute(Sender: TObject);
begin
  Close;
end;

procedure TfrmMain.actConnectExecute(Sender: TObject);
var
  s: String;
begin
  vIntSrv:= GetIntegrationServiceSoap(false, frmProperties.ConnectionString, nil);
  vSession:= vIntSrv.OpenSession('Поликлиника №3', frmProperties.Login, frmProperties.Password);
  if vSession.Result <> -1 then
  begin
    FConnected:= true;
    actFillOrg.Execute;
    StatusBar1.Panels[1].Text:= 'Подключено: '+frmProperties.Login+' ('+frmProperties.ConnectionString+')';
  end else
  begin
    s:= vSession.ErrorMessage;
    MessageBox(handle, PChar('Ошибка при подключении. Сообщение: '#13#10+s), PChar(Application.title), MB_OK);
  end;
end;

procedure TfrmMain.actDeleteOrgExecute(Sender: TObject);
var
  s: string;
begin
  s:= OrgUnit(tvOrg.Selected.Data).NAME_;
  if MessageBox(handle, PChar('Вы уверены что хотите удалить организацию "'+s+'"'), PChar(Application.Title), MB_ICONQUESTION or MB_YESNO) = idYes then
  begin
    vIntSrv.DeleteOrgUnit(vSession.Value.SessionID, OrgUnit(tvOrg.Selected.Data).ID);
    actFillOrg.Execute;
  end;
end;

procedure TfrmMain.actDeletePersonExecute(Sender: TObject);
begin
  vIntSrv.DeletePerson(vSession.Value.SessionID, lvPers.Selected.SubItems[2]);
end;

procedure TfrmMain.actEditPersExecute(Sender: TObject);
var
  Pers: PersonWithPhoto;
  arCard: ArrayOfIdentifier;
  memStream: TMemoryStream;
  i, j: integer;
  EditSession: GuidResult;
  buff: TByteDynArray;
  PersCard: Identifier;
  fName: string;
begin
  PersCard:= nil;
  frmPersonal:= TfrmPersonal.Create(self);
  try
    Pers:= vIntSrv.GetPerson(vSession.Value.SessionID, lvPers.Selected.SubItems[2]);
    frmPersonal.edtFirstName.Text:= Pers.FIRST_NAME;
    frmPersonal.edtLastName.Text:= Pers.Last_NAME;
    frmPersonal.edtMiddleName.Text:= Pers.Middle_NAME;
    frmPersonal.edtTabNum.Text:= Pers.TAB_NUM;
    frmPersonal.SelectedOrgID:= Pers.ORG_ID;
    frmPersonal.edtOrg.Text:= GetOrgNameByID(Pers.ORG_ID);
    FillcbAccessGroup(frmPersonal.cbAccessGroup);
    //Устанавливаем активную группу доступа
    try
      arCard:= vIntSrv.GetPersonIdentifiers(vSession.Value.SessionID, Pers.ID);
      for i := 0 to length(arCard) - 1 do
        if arCard[i].IS_PRIMARY then
        begin
          frmPersonal.edtKey.Text:= arCard[i].CODE;
          for j := 0 to frmPersonal.cbAccessGroup.Items.Count - 1 do
          begin
            if AccessGroup(frmPersonal.cbAccessGroup.Items.Objects[j]).ID = arCard[i].ACCGROUP_ID then
            begin
              frmPersonal.cbAccessGroup.ItemIndex:= j;
              break;
            end;
          end;
        end;
    except
      frmPersonal.edtKey.Text:= '';
      frmPersonal.cbAccessGroup.ItemIndex:= 0;
    end;
    //Фотка
   MemStream:= TMemoryStream.Create;
    try
      MemStream.Write(Pers.PHOTO[0], length(Pers.PHOTO));
      memStream.Position:= 0;
      //PhysicalResolveFileType
      fName:= GetParsecTempFileName;
      delete(fname, Pos('.', fname), length(fname));
      fName:= fName + '.'+PhysicalResolveFileType(MemStream);
//      frmPersonal.Image1.Picture.Bitmap.LoadFromStream(MemStream);
      if fName[length(fname)] <> '.' then
      begin
        MemStream.SaveToFile(fname);
        frmPersonal.Image1.Picture.LoadFromFile(fName);
        DeleteFile(fName);
      end;
    finally
      MemStream.Free;
    end;
    if frmPersonal.ShowModal = mrOk then
    begin
      EditSession:= vIntSrv.OpenPersonEditingSession(vSession.Value.SessionID, Pers.ID);

      pers.FIRST_NAME:= frmPersonal.edtFirstName.Text;
      pers.LAST_NAME:= frmPersonal.edtLastName.Text;
      pers.MIDDLE_NAME:= frmPersonal.edtMiddleName.Text;
      pers.TAB_NUM:= frmPersonal.edtTABNUM.Text;
      pers.ORG_ID:= frmPersonal.SelectedOrgID;

      if  Assigned(frmPersonal.Image1.Picture.Graphic) then
      begin
        memStream:= TMemoryStream.Create;
        frmPersonal.Image1.Picture.Graphic.SaveToStream(memStream);
        setLength(buff, memStream.Size);
        memStream.Position:= 0;
        memStream.Read(buff[0], memStream.Size);
        pers.PHOTO:= buff;
        memStream.Free;
      end;

      vIntSrv.SavePerson(EditSession.Value, pers);
      vIntSrv.ClosePersonEditingSession(EditSession.Value);

      if frmPersonal.edtKey.Text <> '' then
      begin
        //Удаляем карты
        try
          arCard:= vIntSrv.GetPersonIdentifiers(vSession.Value.SessionID, Pers.ID);
          for i := 0 to length(arCard) - 1 do
            vIntSrv.DeleteIdentifier(vSession.Value.SessionID, arCard[i].CODE);
        except
        end;

        EditSession:= vIntSrv.OpenPersonEditingSession(vSession.Value.SessionID, Pers.ID);
        //Заводим карту по новой
        PersCard:= Identifier.Create;
        PersCard.CODE:= frmPersonal.edtKey.Text;
        if frmPersonal.cbAccessGroup.ItemIndex >= 0 then
          PersCard.ACCGROUP_ID:= AccessGroup(frmPersonal.cbAccessGroup.Items.Objects[frmPersonal.cbAccessGroup.ItemIndex]).ID;
        vIntSrv.AddPersonIdentifier(EditSession.Value, PersCard);
      end;

      vIntSrv.ClosePersonEditingSession(EditSession.Value);
    end;
  finally
    frmPersonal.Free;
  end;
  tvOrgClick(nil);
end;

procedure TfrmMain.actEditPersUpdate(Sender: TObject);
begin
  tAction(Sender).Enabled:= not(lvPers.Selected = nil) and FConnected;
end;

procedure TfrmMain.actFillOrgExecute(Sender: TObject);
var
  arOrgUnit: ArrayOfOrgUnit;
  i: integer;
  li: TListItem;
begin
  FillTreeViewOrg(tvOrg);
  tvOrg.FullExpand;
end;

procedure TfrmMain.actFillOrgUpdate(Sender: TObject);
begin
  TAction(Sender).Enabled:= FConnected;
end;

procedure TfrmMain.actPropertiesExecute(Sender: TObject);
begin
  frmProperties.ShowModal;
end;

procedure TfrmMain.actStartEventPoolExecute(Sender: TObject);
var
  dtFrom,dtTo: TXSDateTime;
  Events: ArrayOfEventObject;
  reportParams: EventHistoryQueryParams;
  li: TListItem;
  i: integer;
  eventSession: GuidResult;
  dt: TDateTime;
  Count: Integer;
  OrgID: Guid;
begin
  pcMain.ActivePageIndex := 1;
  lvEvent.Items.Clear;
  dtFrom:= TXSDateTime.Create;
  dtTo:= TXSDateTime.Create;
  frmSelectReportParam:= TfrmSelectReportParam.Create(self);
  try
    if tvOrg.Selected <> nil then
      frmSelectReportParam.SelectedOrgID := OrgUnit(tvOrg.Selected.Data).ID
    else
      frmSelectReportParam.SelectedOrgID := vSession.Value.RootOrgUnitID;

    OrgID := frmSelectReportParam.SelectedOrgID;
    frmSelectReportParam.edtOrg.Text:= vIntSrv.GetOrgUnit(vSession.Value.SessionID, OrgID).NAME_;

    frmSelectReportParam.SelectedTerritoryID:= GUID_EMPTY;
    frmSelectReportParam.edtTerritory.text:= 'Все территории';

    if frmSelectReportParam.ShowModal = mrOK then
    begin
      frmSelectReportParam.FormatDTP;
      dtTo.AsDateTime:= frmSelectReportParam.ToDT;
      dtFrom.AsDateTime:= frmSelectReportParam.FromDT;

      reportParams := EventHistoryQueryParams.Create;
      reportParams.StartDate := dtFrom;
      reportParams.EndDate := dtTo;
      if (frmSelectReportParam.SelectedTerritoryID <> GUID_EMPTY) then
        reportParams.Territories := ArrayOfGuid.Create(frmSelectReportParam.SelectedTerritoryID);

      reportParams.Organizations := ArrayOfGuid.Create(frmSelectReportParam.SelectedOrgID);
      reportParams.EventsWithoutUser := False;
      reportParams.TransactionTypes := ArrayOfUnsignedInt.Create(590144, 590152, 65867, 590165, 590145, 590153, 65866, 590166);
      reportParams.ParentEventID := TGuid.Empty.ToString;
      reportParams.MaxResultSize := 500;

      eventSession := vIntSrv.OpenEventHistorySession(vSession.Value.SessionID, reportParams);
      if eventSession.Result = 0 then
      begin
        Count := vIntSrv.GetEventHistoryResultCount(vSession.Value.SessionID, eventSession.Value);
        StatusBar1.Panels[0].Text:= 'Возвращено строк : ' + Count.ToString;
        if (Count > 0) then
        begin
          Events:= vIntSrv.GetEventHistoryResult(vSession.Value.SessionID, eventSession.Value, ArrayOfGuid.Create('2C5EE108-28E3-4dcc-8C95-7F3222D8E67F', 'D1847AFF-11AA-4ef2-AAAA-795CEEFE5F9F', '1BF8A893-7D21-4c0c-9A2D-2E333A2D769D', '0de358e0-c91b-4333-b902-000000000005', '4C5807CB-2C06-4725-9243-747E40C41D6C', 'C4AE9465-8375-4169-BA61-EB7E365A7352'), 0, 5000);
          for i:= 0 to length(Events)-1 do
          begin
            li:= lvEvent.Items.Add;
            dt := StrToDateTime(Events[i].Values[0]);
            li.Caption:= FormatDateTime('dd.mm.yyyy hh:nn:ss', dt);
            li.SubItems.Add(Events[i].Values[1]);
            li.SubItems.Add(Events[i].Values[2]);
            li.SubItems.Add(Events[i].Values[3]);
            li.SubItems.Add(Events[i].Values[4]);
          end;
        end
        else
          MessageBox(handle, PChar('Нет данных для построения отчёта'), PChar('Операция завершена'), MB_OK+MB_ICONINFORMATION);
        vIntSrv.CloseEventHistorySession(vSession.Value.SessionID, eventSession.Value);
        pcMain.ActivePageIndex:= 1;
      end;
    end;
  finally
    frmSelectReportParam.Free;
    dtFrom.Free;
    dtTo.Free;
  end;
end;

end.
