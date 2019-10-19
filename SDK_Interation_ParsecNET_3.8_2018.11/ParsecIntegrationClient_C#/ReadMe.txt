##########################################################################
#  Описание содержания тестового клиента к сервису интеграции ParsecNET3 #
##########################################################################

ParsecIntegrationClient.csproj - файл проекта.

AccessGroupsListForm.cs - форма со списком групп доступа.

app.config - файл конфигурации приложения.
В данном файле интересна секция applicationSettings, содержащая адрес
сервиса интеграции (http://localhost:10101/IntegrationService/IntegrationService.asmx).

ClientState.cs - класс, хранящий статус клиента (ключ сессии, ...).

CommonFunctions.cs - класс с набором общих функций.

EventsForm.cs - форма для получения событий системы.

Form1.cs - тестовая форма.

IdentifierForm.cs - форма карточки идентификатора.

LoginForm.cs - форма логина в систему.

MainForm.cs - форма со списком персонала.

OrgUnitForm.cs - форма карточки подразделения.

OrgUnitHierarhyForm.cs - форма иерархии подразделений.

PersonForm.cs - форма карточки сотрудника.

PersonHierarhy.cs - форма полной(с сотрудниками и подразделениями) иерархии персонала.

Program.cs - класс, содержащий метод main.

TerritoryHierarhy.cs - класс иерархии территории.

Исходный код классов, используемых в коде (IntegrationService, OrgUnit, BaseObject, BasePerson,...),
находится в файле "Web References\IntegrationWebService\Reference.cs".