object frmSelectReportParam: TfrmSelectReportParam
  Left = 0
  Top = 0
  BorderStyle = bsDialog
  BorderWidth = 4
  Caption = #1055#1072#1088#1072#1084#1077#1090#1088#1099' '#1086#1090#1095#1077#1090#1072
  ClientHeight = 194
  ClientWidth = 331
  Color = clBtnFace
  Font.Charset = DEFAULT_CHARSET
  Font.Color = clWindowText
  Font.Height = -11
  Font.Name = 'Tahoma'
  Font.Style = []
  OldCreateOrder = False
  Position = poScreenCenter
  OnCreate = FormCreate
  DesignSize = (
    331
    194)
  PixelsPerInch = 96
  TextHeight = 13
  object Panel1: TPanel
    Left = 0
    Top = 0
    Width = 331
    Height = 163
    Align = alTop
    Anchors = [akLeft, akTop, akRight, akBottom]
    TabOrder = 0
    object Label4: TLabel
      Left = 16
      Top = 17
      Width = 83
      Height = 13
      Caption = #1053#1072#1095#1072#1083#1086' '#1087#1077#1088#1080#1086#1076#1072
    end
    object Label5: TLabel
      Left = 16
      Top = 44
      Width = 102
      Height = 13
      Caption = #1054#1082#1086#1085#1095#1072#1085#1080#1077' '#1087#1077#1088#1080#1086#1076#1072
    end
    object Label1: TLabel
      Left = 16
      Top = 71
      Width = 66
      Height = 13
      Caption = #1054#1088#1075#1072#1085#1080#1079#1072#1094#1080#1103
    end
    object btnBrowseOrg: TSpeedButton
      Left = 282
      Top = 68
      Width = 23
      Height = 22
      Caption = '...'
      Flat = True
      OnClick = btnBrowseOrgClick
    end
    object Label2: TLabel
      Left = 16
      Top = 98
      Width = 60
      Height = 13
      Caption = #1058#1077#1088#1088#1080#1090#1086#1088#1080#1103
    end
    object btnBrowseTerritory: TSpeedButton
      Left = 282
      Top = 95
      Width = 23
      Height = 22
      Caption = '...'
      Flat = True
      OnClick = btnBrowseTerritoryClick
    end
    object dtpFrom: TDateTimePicker
      Left = 136
      Top = 14
      Width = 81
      Height = 21
      Date = 40273.540583402780000000
      Time = 40273.540583402780000000
      TabOrder = 0
    end
    object dtpTo: TDateTimePicker
      Left = 136
      Top = 41
      Width = 81
      Height = 21
      Date = 40273.540583402780000000
      Time = 40273.540583402780000000
      TabOrder = 1
    end
    object edtOrg: TEdit
      Left = 136
      Top = 68
      Width = 145
      Height = 21
      ReadOnly = True
      TabOrder = 2
    end
    object edtTerritory: TEdit
      Left = 136
      Top = 95
      Width = 145
      Height = 21
      ReadOnly = True
      TabOrder = 3
    end
    object dtpFromTime: TDateTimePicker
      Left = 223
      Top = 14
      Width = 81
      Height = 21
      Date = 40273.540583402780000000
      Time = 40273.540583402780000000
      Kind = dtkTime
      TabOrder = 4
    end
    object dtpToTime: TDateTimePicker
      Left = 223
      Top = 41
      Width = 81
      Height = 21
      Date = 40273.540583402780000000
      Time = 40273.540583402780000000
      Kind = dtkTime
      TabOrder = 5
    end
  end
  object btnOK: TButton
    Left = 175
    Top = 169
    Width = 75
    Height = 25
    Anchors = [akRight, akBottom]
    Caption = 'OK'
    Default = True
    ModalResult = 1
    TabOrder = 1
  end
  object btnCancel: TButton
    Left = 256
    Top = 169
    Width = 75
    Height = 25
    Anchors = [akRight, akBottom]
    Cancel = True
    Caption = #1054#1090#1084#1077#1085#1072
    ModalResult = 2
    TabOrder = 2
  end
end
