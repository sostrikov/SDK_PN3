object frmPersonal: TfrmPersonal
  Left = 0
  Top = 0
  BorderWidth = 4
  Caption = #1057#1086#1090#1088#1091#1076#1085#1080#1082
  ClientHeight = 324
  ClientWidth = 460
  Color = clBtnFace
  Font.Charset = DEFAULT_CHARSET
  Font.Color = clWindowText
  Font.Height = -11
  Font.Name = 'Tahoma'
  Font.Style = []
  OldCreateOrder = False
  Position = poScreenCenter
  DesignSize = (
    460
    324)
  PixelsPerInch = 96
  TextHeight = 13
  object btnCancel: TButton
    Left = 385
    Top = 299
    Width = 75
    Height = 25
    Anchors = [akRight, akBottom]
    Cancel = True
    Caption = #1054#1090#1084#1077#1085#1072
    ModalResult = 2
    TabOrder = 0
  end
  object btnOK: TButton
    Left = 304
    Top = 299
    Width = 75
    Height = 25
    Anchors = [akRight, akBottom]
    Caption = 'OK'
    Default = True
    ModalResult = 1
    TabOrder = 1
  end
  object Panel1: TPanel
    Left = 0
    Top = 0
    Width = 460
    Height = 293
    Align = alTop
    Anchors = [akLeft, akTop, akRight, akBottom]
    TabOrder = 2
    object Bevel1: TBevel
      Left = 11
      Top = 14
      Width = 155
      Height = 176
    end
    object Label1: TLabel
      Left = 176
      Top = 24
      Width = 44
      Height = 13
      Caption = #1060#1072#1084#1080#1083#1080#1103
    end
    object Label2: TLabel
      Left = 176
      Top = 51
      Width = 19
      Height = 13
      Caption = #1048#1084#1103
    end
    object Label3: TLabel
      Left = 176
      Top = 78
      Width = 49
      Height = 13
      Caption = #1054#1090#1095#1077#1089#1090#1074#1086
    end
    object Label4: TLabel
      Left = 176
      Top = 105
      Width = 36
      Height = 13
      Caption = #1058#1072#1073#1077#1083#1100
    end
    object Label5: TLabel
      Left = 176
      Top = 132
      Width = 66
      Height = 13
      Caption = #1054#1088#1075#1072#1085#1080#1079#1072#1094#1080#1103
    end
    object SpeedButton1: TSpeedButton
      Left = 418
      Top = 129
      Width = 23
      Height = 22
      Caption = '...'
      Flat = True
      OnClick = SpeedButton1Click
    end
    object SpeedButton2: TSpeedButton
      Left = 11
      Top = 191
      Width = 23
      Height = 22
      Flat = True
      Glyph.Data = {
        36030000424D3603000000000000360000002800000010000000100000000100
        18000000000000030000C40E0000C40E00000000000000000000800080800080
        8000808000808000808000808000808000808000808000808000808000808000
        8080008080008080008082B8CA53B3D53E98B94288A280008080008080008080
        008080008080008080008080008080008080008080008080008072B2C887D6F1
        88E5FF64D7FF57C9F245B6DD3D9FC2448EA88000808000808000808000808000
        8080008080008080008070B5CE61C5EBA3EAFD79E2FF7FE4FF81E6FF7CE6FF73
        E3FF64D6F750C2E343AAC83789A380008080008080008080008086BFD644B8E8
        A9E9F686ECFF84EAFE85EBFE86EBFF86ECFF89EEFF8AF0FF8BF3FF5BD7FB417C
        8E80008080008080008089CEE942BAED8BD5ECA0FAFF8CF4FE8EF4FE8FF6FF92
        F8FF89F1FB8FF6FF8EF4FF8CF0FF44A5BE80008080008080008096D4EC57C4F8
        6AC5E9B7FFFD93FFFF99FFFF96FCFD7CE2ED5CB4DF79DFEF9AFFFF9AF2FF87E0
        F14D7B8A80008080008089CDE875D3FF50BFEEB0E7F3BBFFFFAAF9FE7AD3EE6F
        D6EFABDEFC73BCE59AF6F9A3F2FFCAFFFF4999B18000808000808CCFE981E0FF
        70DBFD58C4EA5490A889AFB37EB7C49CE1FF97E7FFAED9F87BC7E6B1F2FFEAFF
        FF90D7E7577B8780008094D3EA93F1FF88EFFF87F0FC6188909A8B7F975A0690
        A88996E1EFAFE4F890C7EAB6EBFFF5FFFFF0FFFF5092A880008088D3E9A4FEFF
        95FCFF98FFFF7FDBDE798A8EDE9E60E47B00A9862F7C937596C9CD5C91A572A8
        BCA1D7E862B6D38000809BD7EBADFFFF94FFFF97FFFFA4FFFF83ADB2948F8EA7
        6925BF6400FEB231FFF4B69B99973D41448000808000808000809AC1D0ADEDFA
        AEFBFEA7FEFFB6F8FD91BFCF6F7880B09983E87D03FE9D09D2AD57C1C1AB6767
        688E8C8C80008080008080008092BDCC9BC2D0B0CFD9A3CCDBB6C0C4ADACAB88
        8C90C49158AF5B00C8810DC5B077818585800080800080800080800080800080
        8000808000808000808000808000808B8D8F8F8C89AC671AB18445ADAAA58000
        8080008080008080008080008080008080008080008080008080008080008080
        0080858787969595800080800080800080800080800080800080}
      OnClick = SpeedButton2Click
    end
    object Image1: TImage
      Left = 16
      Top = 18
      Width = 145
      Height = 167
      Stretch = True
    end
    object Label6: TLabel
      Left = 176
      Top = 159
      Width = 28
      Height = 13
      Caption = #1050#1083#1102#1095
    end
    object Label7: TLabel
      Left = 176
      Top = 191
      Width = 81
      Height = 13
      Caption = #1043#1088#1091#1087#1087#1072' '#1076#1086#1089#1090#1091#1087#1072
    end
    object edtFirstName: TEdit
      Left = 240
      Top = 21
      Width = 201
      Height = 21
      TabOrder = 0
    end
    object edtLastName: TEdit
      Left = 240
      Top = 48
      Width = 201
      Height = 21
      TabOrder = 1
    end
    object edtMiddleName: TEdit
      Left = 240
      Top = 75
      Width = 201
      Height = 21
      TabOrder = 2
    end
    object edtTabNum: TEdit
      Left = 240
      Top = 102
      Width = 201
      Height = 21
      TabOrder = 3
    end
    object edtOrg: TEdit
      Left = 280
      Top = 129
      Width = 137
      Height = 21
      ReadOnly = True
      TabOrder = 4
    end
    object edtKey: TEdit
      Left = 240
      Top = 156
      Width = 201
      Height = 21
      TabOrder = 5
      Text = 'F2B9E804'
    end
    object cbAccessGroup: TComboBox
      Left = 280
      Top = 188
      Width = 161
      Height = 21
      Style = csDropDownList
      ItemHeight = 13
      TabOrder = 6
    end
  end
  object OpenPictureDialog1: TOpenPictureDialog
    Left = 72
    Top = 200
  end
end
