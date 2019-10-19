program DelphiClient;

uses
  Forms,
  uMain in 'uMain.pas' {frmMain},
  IntegrationService in 'IntegrationService.pas',
  uPersonal in 'uPersonal.pas' {frmPersonal},
  uSelectOrg in 'uSelectOrg.pas' {frmSelectOrg},
  uProperties in 'uProperties.pas' {frmProperties},
  uSelectReportParam in 'uSelectReportParam.pas' {frmSelectReportParam},
  uSelectTerritory in 'uSelectTerritory.pas' {frmSelectTerritory},
  zlibpas in 'PNGImage\zlibpas.pas',
  pngextra in 'PNGImage\pngextra.pas',
  pngimage in 'PNGImage\pngimage.pas',
  pnglang in 'PNGImage\pnglang.pas';

{$R *.res}

begin
  Application.Initialize;
  Application.MainFormOnTaskbar := True;
  Application.CreateForm(TfrmMain, frmMain);
  Application.CreateForm(TfrmProperties, frmProperties);
  Application.OnHint:= frmMain.HintPanel;
  Application.Run;
end.
