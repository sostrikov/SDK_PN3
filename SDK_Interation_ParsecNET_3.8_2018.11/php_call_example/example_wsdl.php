<?

// Example of ParsecIntegrationServiceCall throught php SoapClient class
$client = new SoapClient("http://127.0.0.1:10101/IntegrationService/IntegrationService.asmx?WSDL", array( 'soap_version' => SOAP_1_2));
printf('<h2>GetVersion result</h2>');
var_dump($client->GetVersion());
printf('<h2>OpenSession result</h2>');
printf($info);
$params = array("domain" => "SYSTEM", "userName" => "parsec", "password" => "parsec");
var_dump($client->OpenSession($params));
printf('<br>');

?>