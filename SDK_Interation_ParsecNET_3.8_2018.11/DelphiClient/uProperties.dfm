object frmProperties: TfrmProperties
  Left = 0
  Top = 0
  BorderStyle = bsDialog
  BorderWidth = 4
  Caption = #1053#1072#1089#1090#1088#1086#1081#1082#1080
  ClientHeight = 384
  ClientWidth = 597
  Color = clBtnFace
  Font.Charset = DEFAULT_CHARSET
  Font.Color = clWindowText
  Font.Height = -11
  Font.Name = 'Tahoma'
  Font.Style = []
  OldCreateOrder = False
  Position = poScreenCenter
  OnCloseQuery = FormCloseQuery
  OnCreate = FormCreate
  DesignSize = (
    597
    384)
  PixelsPerInch = 96
  TextHeight = 13
  object Panel1: TPanel
    Left = 0
    Top = 0
    Width = 597
    Height = 355
    Align = alTop
    Anchors = [akLeft, akTop, akRight, akBottom]
    BevelOuter = bvNone
    TabOrder = 0
    object Splitter1: TSplitter
      Left = 152
      Top = 0
      Height = 355
      ExplicitLeft = 296
      ExplicitTop = 168
      ExplicitHeight = 100
    end
    object tvCategories: TTreeView
      Left = 0
      Top = 0
      Width = 152
      Height = 355
      Align = alLeft
      Indent = 19
      ReadOnly = True
      TabOrder = 0
      OnChange = tvCategoriesChange
      Items.NodeData = {
        010300000023000000000000000000000000000000FFFFFFFF00000000000000
        00051E0431044904380435042F000000000000000000000001000000FFFFFFFF
        00000000000000000B1F043E0434043A043B044E04470435043D04380435042F
        000000000000000000000002000000FFFFFFFF00000000010000000B1A043E04
        3D0441044204400443043A0442043E0440041F00000000000000000000000300
        0000FFFFFFFF000000000000000003120438043404}
    end
    object pcProp: TPageControl
      Left = 155
      Top = 0
      Width = 442
      Height = 355
      ActivePage = tsConnection
      Align = alClient
      TabOrder = 1
      object tabMain: TTabSheet
        Caption = 'tabMain'
      end
      object tsConnection: TTabSheet
        Caption = 'tsConnection'
        ImageIndex = 1
        DesignSize = (
          434
          327)
        object Bevel1: TBevel
          Left = 3
          Top = 3
          Width = 428
          Height = 142
          Anchors = [akLeft, akTop, akRight]
          Shape = bsFrame
          ExplicitWidth = 426
        end
        object Label1: TLabel
          Left = 24
          Top = 24
          Width = 110
          Height = 13
          Caption = #1057#1090#1088#1086#1082#1072' '#1087#1086#1076#1082#1083#1102#1095#1077#1085#1080#1103
        end
        object Label2: TLabel
          Left = 24
          Top = 64
          Width = 72
          Height = 13
          Caption = #1055#1086#1083#1100#1079#1086#1074#1072#1090#1077#1083#1100
        end
        object Label3: TLabel
          Left = 24
          Top = 99
          Width = 37
          Height = 13
          Caption = #1055#1072#1088#1086#1083#1100
        end
        object edtConnectString: TEdit
          Left = 153
          Top = 21
          Width = 250
          Height = 21
          Anchors = [akLeft, akTop, akRight]
          TabOrder = 0
          Text = 'edtConnectString'
        end
        object edtLogin: TEdit
          Left = 153
          Top = 61
          Width = 250
          Height = 21
          Anchors = [akLeft, akTop, akRight]
          TabOrder = 1
          Text = 'edtLogin'
        end
        object edtPassword: TMaskEdit
          Left = 153
          Top = 96
          Width = 250
          Height = 21
          Anchors = [akLeft, akTop, akRight]
          PasswordChar = '*'
          TabOrder = 2
          Text = 'edtPassword'
        end
      end
      object tsConstructor: TTabSheet
        Caption = 'tsConstructor'
        ImageIndex = 2
      end
      object tsView: TTabSheet
        Caption = 'tsView'
        ImageIndex = 3
      end
    end
  end
  object btnOk: TButton
    Left = 441
    Top = 357
    Width = 75
    Height = 25
    Anchors = [akRight, akBottom]
    Caption = 'OK'
    Default = True
    ModalResult = 1
    TabOrder = 1
  end
  object btnCancel: TButton
    Left = 522
    Top = 357
    Width = 75
    Height = 25
    Anchors = [akRight, akBottom]
    Cancel = True
    Caption = #1054#1090#1084#1077#1085#1072
    ModalResult = 2
    TabOrder = 2
  end
  object XMLDoc: TXMLDocument
    Left = 184
    Top = 352
    DOMVendorDesc = 'MSXML'
  end
end
