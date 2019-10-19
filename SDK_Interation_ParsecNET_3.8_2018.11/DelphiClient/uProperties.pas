unit uProperties;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, CategoryButtons, ExtCtrls, ComCtrls, StdCtrls, Mask, Buttons, FileCtrl,
  xmldom, XMLIntf, msxmldom, XMLDoc;

type
  TfrmProperties = class(TForm)
    Panel1: TPanel;
    tvCategories: TTreeView;
    Splitter1: TSplitter;
    pcProp: TPageControl;
    btnOk: TButton;
    btnCancel: TButton;
    tabMain: TTabSheet;
    tsConnection: TTabSheet;
    Bevel1: TBevel;
    Label1: TLabel;
    edtConnectString: TEdit;
    Label2: TLabel;
    edtLogin: TEdit;
    Label3: TLabel;
    edtPassword: TMaskEdit;
    XMLDoc: TXMLDocument;
    tsConstructor: TTabSheet;
    tsView: TTabSheet;
    procedure FormCreate(Sender: TObject);
    procedure tvCategoriesChange(Sender: TObject; Node: TTreeNode);
    procedure Button1Click(Sender: TObject);
    procedure FormCloseQuery(Sender: TObject; var CanClose: Boolean);
  private
    FParamFileName: string;
    //aName - название параметра с путем через '\'
    //без нулевого уровн€ (configuration). Ќапример: 'connection\connection_string'
    function FGetPropertyStr(aName: string): string;
    function FSetPropertyStr(aName, aValue: string): string;


    procedure FSetConnStr(aValue: string);
    function FGetConnStr: string;
    procedure FSetLogin(aValue: string);
    function FGetLogin: string;
    procedure FSetPassword(aValue: string);
    function FGetPassword: string;
    procedure FSetWelcomePage(aValue: string);
    function FGetWelcomePage: string;
  public
    { Public declaration }
    property ConnectionString: string read FGetConnStr write FSetConnStr;
    property Login: string read FGetLogin write FSetLogin;
    property Password: string read FGetPassword write FSetPassword;
    property WelcomePage: string read FGetWelcomePage write FSetWelcomePage;

    procedure FillParamEditors;
    procedure SaveEditedValues;
  end;

var
  frmProperties: TfrmProperties;

implementation

{$R *.dfm}

procedure TfrmProperties.FSetLogin(aValue: string);
begin
  FSetPropertyStr('connection\user', aValue);
end;

function TfrmProperties.FGetLogin: string;
begin
  result:= FGetPropertyStr('connection\user');
end;

procedure TfrmProperties.FSetPassword(aValue: string);
begin
  FSetPropertyStr('connection\password', aValue);
end;

function TfrmProperties.FGetPassword: string;
begin
  result:= FGetPropertyStr('connection\password');
end;

procedure TfrmProperties.FSetWelcomePage(aValue: string);
begin
  FSetPropertyStr('view\welcome_page', aValue);
end;

function TfrmProperties.FGetWelcomePage: string;
begin
  result:= FGetPropertyStr('view\welcome_page');
  if result = '' then
    result:= 'welcome\dip_welcome.html';
end;

procedure TfrmProperties.FSetConnStr(aValue: string);
begin
  FSetPropertyStr('connection\connection_string', aValue);
end;

function TfrmProperties.FGetConnStr: string;
begin
  result:= FGetPropertyStr('connection\connection_string');
end;

function TfrmProperties.FGetPropertyStr(aName: string): string;
var
  aNode: IXMLNode;
begin
  result:= '';
  XMLDoc.Active:= true;
  try
    //RootNode
    aNode:= XMLDoc.ChildNodes.FindNode('configuration');
    if aNode <> nil then
    begin
      while Pos('\', aName) > 0 do
      begin
        aNode:= aNode.ChildNodes.FindNode(copy(aName, 1, Pos('\', aName)-1));
        delete(aName, 1, Pos('\', aName));
        if aNode = nil then
          exit;
      end;
      if aNode.ChildNodes.FindNode(aName) <> nil then
        result:= aNode.ChildNodes.FindNode(aName).Text;
    end;
  finally
    XMLDoc.Active:= false;
  end;
end;

function TfrmProperties.FSetPropertyStr(aName, aValue: string): string;
var
  aNode, aChildNode: IXMLNode;
begin
  result:= '';
  XMLDoc.Active:= true;
  try
    //RootNode
    aNode:= XMLDoc.ChildNodes.FindNode('configuration');
    if aNode <> nil then
    begin
      while Pos('\', aName) > 0 do
      begin
        aChildNode:= aNode.ChildNodes.FindNode(copy(aName, 1, Pos('\', aName)-1));
        //≈сли узла не существует, то создаем его
        if aChildNode = nil then
          aChildNode:= aNode.AddChild(copy(aName, 1, Pos('\', aName)-1), 0);
        aNode:= aChildNode;
        delete(aName, 1, Pos('\', aName));
      end;
      //“еперь ищем существующий или пишем во
      //вновьсозданный параметр его значение
      if aNode.ChildNodes.FindNode(aName) <> nil then
      begin
        aNode.ChildNodes.FindNode(aName).Text:= aValue
      end else
      begin
        aChildNode:= aNode.AddChild(aName, 0);
        aChildNode.Text:= aValue;
      end;
      XMLDoc.SaveToFile(XMLDoc.FileName);
    end;
  finally
    XMLDoc.Active:= false;
  end;
end;

procedure TfrmProperties.Button1Click(Sender: TObject);
begin
  FSetPropertyStr('connection\newparamset\newparam2', 'param2_value');
//  ShowMessage(FGetPropertyStr('connection\connection_string'));
end;

procedure TfrmProperties.FormCloseQuery(Sender: TObject; var CanClose: Boolean);
begin
  if ModalResult = mrOK then
    SaveEditedValues;
end;

procedure TfrmProperties.tvCategoriesChange(Sender: TObject; Node: TTreeNode);
begin
  pcProp.ActivePageIndex:= Node.StateIndex;
end;

procedure TfrmProperties.FormCreate(Sender: TObject);
var
  i: integer;
begin
  for I := 0 to pcProp.PageCount - 1 do
    pcProp.Pages[i].TabVisible:= false;
  try
    FParamFileName:= ExtractFilePath(Application.ExeName)+'\'+ExtractFileName(Application.ExeName)+'.config';
    if not FileExists(FParamFileName) then
      Exit;
    XMLDoc.FileName:= FParamFileName;
    FillParamEditors;

    tvCategories.FullExpand;
    //¬ыбираем начало дерева
    tvCategories.Selected:= tvCategories.Items[1];
    tvCategoriesChange(nil, tvCategories.Items[1]);
  except

  end;
end;

procedure TfrmProperties.FillParamEditors;
begin
  edtConnectString.Text:= FGetPropertyStr('connection\connection_string');
  edtLogin.Text:= FGetPropertyStr('connection\user');
  edtPassword.Text:= FGetPropertyStr('connection\password');
end;

procedure TfrmProperties.SaveEditedValues;
begin
  FSetPropertyStr('connection\connection_string', edtConnectString.Text);
  FSetPropertyStr('connection\user', edtLogin.Text);
  FSetPropertyStr('connection\password', edtPassword.Text);
end;

end.
