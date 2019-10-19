// ************************************************************************ //
// The types declared in this file were generated from data read from the
// WSDL File described below:
// WSDL     : C:\IntegrationService.asmx.xml
//  >Import : C:\IntegrationService.asmx.xml>0
//  >Import : C:\IntegrationService.asmx.xml>1
// Encoding : utf-8
// Codegen  : [wfForceSOAP12+]
// Version  : 1.0
// (07.04.2014 20:15:45 - - $Rev: 56641 $)
// ************************************************************************ //

unit IntegrationService;

interface

uses InvokeRegistry, SOAPHTTPClient, Types, XSBuiltIns;

const
  IS_OPTN = $0001;
  IS_UNBD = $0002;
  IS_NLBL = $0004;
  IS_REF  = $0080;


type

  // ************************************************************************ //
  // The following types, referred to in the WSDL document are not being represented
  // in this file. They are either aliases[@] of other types represented or were referred
  // to but never[!] declared in the document. The types from the latter category
  // typically map to predefined/known XML or Embarcadero types; however, they could also
  // indicate incorrect WSDL documents that failed to declare or import a schema type.
  // ************************************************************************ //
  // !:base64Binary    - "http://www.w3.org/2001/XMLSchema"[Gbl]
  // !:string          - "http://www.w3.org/2001/XMLSchema"[Gbl]
  // !:int             - "http://www.w3.org/2001/XMLSchema"[Gbl]
  // !:boolean         - "http://www.w3.org/2001/XMLSchema"[Gbl]
  // !:dateTime        - "http://www.w3.org/2001/XMLSchema"[Gbl]
  // !:unsignedInt     - "http://www.w3.org/2001/XMLSchema"[Gbl]
  // !:unsignedByte    - "http://www.w3.org/2001/XMLSchema"[Gbl]

  AccessGroup          = class;                 { "http://parsec.ru/Parsec3IntergationService"[GblCplx] }
  BaseResult           = class;                 { "http://parsec.ru/Parsec3IntergationService"[GblCplx] }
  SessionResult        = class;                 { "http://parsec.ru/Parsec3IntergationService"[GblCplx] }
  GuidResult           = class;                 { "http://parsec.ru/Parsec3IntergationService"[GblCplx] }
  Session              = class;                 { "http://parsec.ru/Parsec3IntergationService"[GblCplx] }
  BaseObject           = class;                 { "http://parsec.ru/Parsec3IntergationService"[GblCplx] }
  BaseOrgUnit          = class;                 { "http://parsec.ru/Parsec3IntergationService"[GblCplx] }
  OrgUnit              = class;                 { "http://parsec.ru/Parsec3IntergationService"[GblCplx] }
  BaseIdentifier       = class;                 { "http://parsec.ru/Parsec3IntergationService"[GblCplx] }
  Identifier           = class;                 { "http://parsec.ru/Parsec3IntergationService"[GblCplx] }
  IdentifierTemp       = class;                 { "http://parsec.ru/Parsec3IntergationService"[GblCplx] }
  StockIdentifier      = class;                 { "http://parsec.ru/Parsec3IntergationService"[GblCplx] }
  BasePerson           = class;                 { "http://parsec.ru/Parsec3IntergationService"[GblCplx] }
  Person               = class;                 { "http://parsec.ru/Parsec3IntergationService"[GblCplx] }
  PersonWithPhoto      = class;                 { "http://parsec.ru/Parsec3IntergationService"[GblCplx] }
  Event                = class;                 { "http://parsec.ru/Parsec3IntergationService"[GblCplx] }
  TimeInterval         = class;                 { "http://parsec.ru/Parsec3IntergationService"[GblCplx] }
  Schedule             = class;                 { "http://parsec.ru/Parsec3IntergationService"[GblCplx] }
  EventObject          = class;                 { "http://parsec.ru/Parsec3IntergationService"[GblCplx] }
  Domain               = class;                 { "http://parsec.ru/Parsec3IntergationService"[GblCplx] }
  VisitorRequest       = class;                 { "http://parsec.ru/Parsec3IntergationService"[GblCplx] }
  EventHistoryQueryParams = class;              { "http://parsec.ru/Parsec3IntergationService"[GblCplx] }
  EventsHistory        = class;                 { "http://parsec.ru/Parsec3IntergationService"[GblCplx] }
  BaseTerritory        = class;                 { "http://parsec.ru/Parsec3IntergationService"[GblCplx] }
  Territory            = class;                 { "http://parsec.ru/Parsec3IntergationService"[GblCplx] }
  EventsHistoryResult  = class;                 { "http://parsec.ru/Parsec3IntergationService"[GblCplx] }
  StringResult         = class;                 { "http://parsec.ru/Parsec3IntergationService"[GblCplx] }
  ExtraFieldValue      = class;                 { "http://parsec.ru/Parsec3IntergationService"[GblCplx] }
  ObjectResult         = class;                 { "http://parsec.ru/Parsec3IntergationService"[GblCplx] }
  PersonExtraFieldTemplate = class;             { "http://parsec.ru/Parsec3IntergationService"[GblCplx] }

  {$SCOPEDENUMS ON}
  { "http://parsec.ru/Parsec3IntergationService"[GblSmpl] }
  XmlTypeCode = (
      None,
      Item,
      Node,
      Document,
      Element,
      Attribute,
      Namespace,
      ProcessingInstruction,
      Comment,
      Text,
      AnyAtomicType,
      UntypedAtomic,
      String_,
      Boolean_,
      Decimal,
      Float,
      Double_,
      Duration,
      DateTime,
      Time,
      Date,
      GYearMonth,
      GYear,
      GMonthDay,
      GDay,
      GMonth,
      HexBinary,
      Base64Binary,
      AnyUri,
      QName,
      Notation,
      NormalizedString,
      Token,
      Language,
      NmToken,
      Name_,
      NCName,
      Id,
      Idref,
      Entity,
      Integer_,
      NonPositiveInteger,
      NegativeInteger,
      Long,
      Int,
      Short,
      Byte_,
      NonNegativeInteger,
      UnsignedLong,
      UnsignedInt,
      UnsignedShort,
      UnsignedByte,
      PositiveInteger,
      YearMonthDuration,
      DayTimeDuration
  );

  {$SCOPEDENUMS OFF}

  ArrayOfOrgUnit = array of OrgUnit;            { "http://parsec.ru/Parsec3IntergationService"[GblCplx] }
  ArrayOfBaseObject = array of BaseObject;      { "http://parsec.ru/Parsec3IntergationService"[GblCplx] }
  ArrayOfTerritory = array of Territory;        { "http://parsec.ru/Parsec3IntergationService"[GblCplx] }
  ArrayOfAccessGroup = array of AccessGroup;    { "http://parsec.ru/Parsec3IntergationService"[GblCplx] }
  ArrayOfIdentifier = array of Identifier;      { "http://parsec.ru/Parsec3IntergationService"[GblCplx] }
  guid            =  type string;               { "http://microsoft.com/wsdl/types/"[GblSmpl] }


  // ************************************************************************ //
  // XML       : AccessGroup, global, <complexType>
  // Namespace : http://parsec.ru/Parsec3IntergationService
  // ************************************************************************ //
  AccessGroup = class(TRemotable)
  private
    FID: guid;
    FNAME_: string;
    FNAME__Specified: boolean;
    procedure SetNAME_(Index: Integer; const Astring: string);
    function  NAME__Specified(Index: Integer): boolean;
  published
    property ID:    guid    read FID write FID;
    property NAME_: string  Index (IS_OPTN) read FNAME_ write SetNAME_ stored NAME__Specified;
  end;



  // ************************************************************************ //
  // XML       : BaseResult, global, <complexType>
  // Namespace : http://parsec.ru/Parsec3IntergationService
  // ************************************************************************ //
  BaseResult = class(TRemotable)
  private
    FResult: Integer;
    FErrorMessage: string;
    FErrorMessage_Specified: boolean;
    procedure SetErrorMessage(Index: Integer; const Astring: string);
    function  ErrorMessage_Specified(Index: Integer): boolean;
  published
    property Result:       Integer  read FResult write FResult;
    property ErrorMessage: string   Index (IS_OPTN) read FErrorMessage write SetErrorMessage stored ErrorMessage_Specified;
  end;



  // ************************************************************************ //
  // XML       : SessionResult, global, <complexType>
  // Namespace : http://parsec.ru/Parsec3IntergationService
  // ************************************************************************ //
  SessionResult = class(BaseResult)
  private
    FValue: Session;
    FValue_Specified: boolean;
    procedure SetValue(Index: Integer; const ASession: Session);
    function  Value_Specified(Index: Integer): boolean;
  public
    destructor Destroy; override;
  published
    property Value: Session  Index (IS_OPTN) read FValue write SetValue stored Value_Specified;
  end;



  // ************************************************************************ //
  // XML       : GuidResult, global, <complexType>
  // Namespace : http://parsec.ru/Parsec3IntergationService
  // ************************************************************************ //
  GuidResult = class(BaseResult)
  private
    FValue: guid;
  published
    property Value: guid  read FValue write FValue;
  end;



  // ************************************************************************ //
  // XML       : Session, global, <complexType>
  // Namespace : http://parsec.ru/Parsec3IntergationService
  // ************************************************************************ //
  Session = class(TRemotable)
  private
    FSessionID: guid;
    FRootOrgUnitID: guid;
    FRootTerritoryID: guid;
  published
    property SessionID:       guid  read FSessionID write FSessionID;
    property RootOrgUnitID:   guid  read FRootOrgUnitID write FRootOrgUnitID;
    property RootTerritoryID: guid  read FRootTerritoryID write FRootTerritoryID;
  end;



  // ************************************************************************ //
  // XML       : BaseObject, global, <complexType>
  // Namespace : http://parsec.ru/Parsec3IntergationService
  // ************************************************************************ //
  BaseObject = class(TRemotable)
  private
  published
  end;



  // ************************************************************************ //
  // XML       : BaseOrgUnit, global, <complexType>
  // Namespace : http://parsec.ru/Parsec3IntergationService
  // ************************************************************************ //
  BaseOrgUnit = class(BaseObject)
  private
    FID: guid;
    FNAME_: string;
    FNAME__Specified: boolean;
    FDESC: string;
    FDESC_Specified: boolean;
    procedure SetNAME_(Index: Integer; const Astring: string);
    function  NAME__Specified(Index: Integer): boolean;
    procedure SetDESC(Index: Integer; const Astring: string);
    function  DESC_Specified(Index: Integer): boolean;
  published
    property ID:    guid    read FID write FID;
    property NAME_: string  Index (IS_OPTN) read FNAME_ write SetNAME_ stored NAME__Specified;
    property DESC:  string  Index (IS_OPTN) read FDESC write SetDESC stored DESC_Specified;
  end;



  // ************************************************************************ //
  // XML       : OrgUnit, global, <complexType>
  // Namespace : http://parsec.ru/Parsec3IntergationService
  // ************************************************************************ //
  OrgUnit = class(BaseOrgUnit)
  private
    FPARENT_ID: guid;
  published
    property PARENT_ID: guid  read FPARENT_ID write FPARENT_ID;
  end;



  // ************************************************************************ //
  // XML       : BaseIdentifier, global, <complexType>
  // Namespace : http://parsec.ru/Parsec3IntergationService
  // ************************************************************************ //
  BaseIdentifier = class(BaseObject)
  private
    FCODE: string;
    FCODE_Specified: boolean;
    FPERSON_ID: guid;
    FIS_PRIMARY: Boolean;
    procedure SetCODE(Index: Integer; const Astring: string);
    function  CODE_Specified(Index: Integer): boolean;
  published
    property CODE:       string   Index (IS_OPTN) read FCODE write SetCODE stored CODE_Specified;
    property PERSON_ID:  guid     read FPERSON_ID write FPERSON_ID;
    property IS_PRIMARY: Boolean  read FIS_PRIMARY write FIS_PRIMARY;
  end;



  // ************************************************************************ //
  // XML       : Identifier, global, <complexType>
  // Namespace : http://parsec.ru/Parsec3IntergationService
  // ************************************************************************ //
  Identifier = class(BaseIdentifier)
  private
    FACCGROUP_ID: guid;
  published
    property ACCGROUP_ID: guid  read FACCGROUP_ID write FACCGROUP_ID;
  end;



  // ************************************************************************ //
  // XML       : IdentifierTemp, global, <complexType>
  // Namespace : http://parsec.ru/Parsec3IntergationService
  // ************************************************************************ //
  IdentifierTemp = class(Identifier)
  private
    FVALID_FROM: TXSDateTime;
    FVALID_TO: TXSDateTime;
  public
    destructor Destroy; override;
  published
    property VALID_FROM: TXSDateTime  read FVALID_FROM write FVALID_FROM;
    property VALID_TO:   TXSDateTime  read FVALID_TO write FVALID_TO;
  end;



  // ************************************************************************ //
  // XML       : StockIdentifier, global, <complexType>
  // Namespace : http://parsec.ru/Parsec3IntergationService
  // ************************************************************************ //
  StockIdentifier = class(IdentifierTemp)
  private
  published
  end;



  // ************************************************************************ //
  // XML       : BasePerson, global, <complexType>
  // Namespace : http://parsec.ru/Parsec3IntergationService
  // ************************************************************************ //
  BasePerson = class(BaseObject)
  private
    FID: guid;
    FFIRST_NAME: string;
    FFIRST_NAME_Specified: boolean;
    FLAST_NAME: string;
    FLAST_NAME_Specified: boolean;
    FMIDDLE_NAME: string;
    FMIDDLE_NAME_Specified: boolean;
    FTAB_NUM: string;
    FTAB_NUM_Specified: boolean;
    procedure SetFIRST_NAME(Index: Integer; const Astring: string);
    function  FIRST_NAME_Specified(Index: Integer): boolean;
    procedure SetLAST_NAME(Index: Integer; const Astring: string);
    function  LAST_NAME_Specified(Index: Integer): boolean;
    procedure SetMIDDLE_NAME(Index: Integer; const Astring: string);
    function  MIDDLE_NAME_Specified(Index: Integer): boolean;
    procedure SetTAB_NUM(Index: Integer; const Astring: string);
    function  TAB_NUM_Specified(Index: Integer): boolean;
  published
    property ID:          guid    read FID write FID;
    property FIRST_NAME:  string  Index (IS_OPTN) read FFIRST_NAME write SetFIRST_NAME stored FIRST_NAME_Specified;
    property LAST_NAME:   string  Index (IS_OPTN) read FLAST_NAME write SetLAST_NAME stored LAST_NAME_Specified;
    property MIDDLE_NAME: string  Index (IS_OPTN) read FMIDDLE_NAME write SetMIDDLE_NAME stored MIDDLE_NAME_Specified;
    property TAB_NUM:     string  Index (IS_OPTN) read FTAB_NUM write SetTAB_NUM stored TAB_NUM_Specified;
  end;



  // ************************************************************************ //
  // XML       : Person, global, <complexType>
  // Namespace : http://parsec.ru/Parsec3IntergationService
  // ************************************************************************ //
  Person = class(BasePerson)
  private
    FORG_ID: guid;
  published
    property ORG_ID: guid  read FORG_ID write FORG_ID;
  end;



  // ************************************************************************ //
  // XML       : PersonWithPhoto, global, <complexType>
  // Namespace : http://parsec.ru/Parsec3IntergationService
  // ************************************************************************ //
  PersonWithPhoto = class(Person)
  private
    FPHOTO: TByteDynArray;
    FPHOTO_Specified: boolean;
    procedure SetPHOTO(Index: Integer; const ATByteDynArray: TByteDynArray);
    function  PHOTO_Specified(Index: Integer): boolean;
  published
    property PHOTO: TByteDynArray  Index (IS_OPTN) read FPHOTO write SetPHOTO stored PHOTO_Specified;
  end;



  // ************************************************************************ //
  // XML       : Event, global, <complexType>
  // Namespace : http://parsec.ru/Parsec3IntergationService
  // ************************************************************************ //
  Event = class(BaseObject)
  private
    FEventDate: TXSDateTime;
    FEventType: Integer;
    FEventPersonIndex: Integer;
    FCODE: string;
    FCODE_Specified: boolean;
    FEventTerritoryIndex: Integer;
    procedure SetCODE(Index: Integer; const Astring: string);
    function  CODE_Specified(Index: Integer): boolean;
  public
    destructor Destroy; override;
  published
    property EventDate:           TXSDateTime  read FEventDate write FEventDate;
    property EventType:           Integer      read FEventType write FEventType;
    property EventPersonIndex:    Integer      read FEventPersonIndex write FEventPersonIndex;
    property CODE:                string       Index (IS_OPTN) read FCODE write SetCODE stored CODE_Specified;
    property EventTerritoryIndex: Integer      read FEventTerritoryIndex write FEventTerritoryIndex;
  end;

  ArrayOfTimeInterval = array of TimeInterval;   { "http://parsec.ru/Parsec3IntergationService"[GblCplx] }


  // ************************************************************************ //
  // XML       : TimeInterval, global, <complexType>
  // Namespace : http://parsec.ru/Parsec3IntergationService
  // ************************************************************************ //
  TimeInterval = class(BaseObject)
  private
    FSTART: TXSDateTime;
    FEND_: TXSDateTime;
  public
    destructor Destroy; override;
  published
    property START: TXSDateTime  read FSTART write FSTART;
    property END_:  TXSDateTime  read FEND_ write FEND_;
  end;

  ArrayOfDomain = array of Domain;              { "http://parsec.ru/Parsec3IntergationService"[GblCplx] }


  // ************************************************************************ //
  // XML       : Schedule, global, <complexType>
  // Namespace : http://parsec.ru/Parsec3IntergationService
  // ************************************************************************ //
  Schedule = class(BaseObject)
  private
    FID: guid;
    FNAME_: string;
    FNAME__Specified: boolean;
    procedure SetNAME_(Index: Integer; const Astring: string);
    function  NAME__Specified(Index: Integer): boolean;
  published
    property ID:    guid    read FID write FID;
    property NAME_: string  Index (IS_OPTN) read FNAME_ write SetNAME_ stored NAME__Specified;
  end;

  ArrayOfVisitorRequest = array of VisitorRequest;   { "http://parsec.ru/Parsec3IntergationService"[GblCplx] }
  ArrayOfPerson = array of Person;              { "http://parsec.ru/Parsec3IntergationService"[GblCplx] }
  ArrayOfSchedule = array of Schedule;          { "http://parsec.ru/Parsec3IntergationService"[GblCplx] }
  ArrayOfAnyType = array of string;             { "http://parsec.ru/Parsec3IntergationService"[GblCplx] }
  ArrayOfEventObject = array of EventObject;    { "http://parsec.ru/Parsec3IntergationService"[GblCplx] }


  // ************************************************************************ //
  // XML       : EventObject, global, <complexType>
  // Namespace : http://parsec.ru/Parsec3IntergationService
  // ************************************************************************ //
  EventObject = class(BaseObject)
  private
    FValues: ArrayOfAnyType;
    FValues_Specified: boolean;
    procedure SetValues(Index: Integer; const AArrayOfAnyType: ArrayOfAnyType);
    function  Values_Specified(Index: Integer): boolean;
  published
    property Values: ArrayOfAnyType  Index (IS_OPTN) read FValues write SetValues stored Values_Specified;
  end;



  // ************************************************************************ //
  // XML       : Domain, global, <complexType>
  // Namespace : http://parsec.ru/Parsec3IntergationService
  // ************************************************************************ //
  Domain = class(BaseObject)
  private
    FNAME_: string;
    FNAME__Specified: boolean;
    FDESCRIPTION: string;
    FDESCRIPTION_Specified: boolean;
    FVISITOR_CONTROL: Boolean;
    FIS_SYSTEM: Boolean;
    procedure SetNAME_(Index: Integer; const Astring: string);
    function  NAME__Specified(Index: Integer): boolean;
    procedure SetDESCRIPTION(Index: Integer; const Astring: string);
    function  DESCRIPTION_Specified(Index: Integer): boolean;
  published
    property NAME_:           string   Index (IS_OPTN) read FNAME_ write SetNAME_ stored NAME__Specified;
    property DESCRIPTION:     string   Index (IS_OPTN) read FDESCRIPTION write SetDESCRIPTION stored DESCRIPTION_Specified;
    property VISITOR_CONTROL: Boolean  read FVISITOR_CONTROL write FVISITOR_CONTROL;
    property IS_SYSTEM:       Boolean  read FIS_SYSTEM write FIS_SYSTEM;
  end;

  ArrayOfUnsignedInt = array of Cardinal;       { "http://parsec.ru/Parsec3IntergationService"[GblCplx] }


  // ************************************************************************ //
  // XML       : VisitorRequest, global, <complexType>
  // Namespace : http://parsec.ru/Parsec3IntergationService
  // ************************************************************************ //
  VisitorRequest = class(TRemotable)
  private
    FID: guid;
    FNUMBER: Integer;
    FDATE: TXSDateTime;
    FORGUNIT_ID: guid;
    FPERSON_ID: guid;
    FPERSON_INFO: string;
    FPERSON_INFO_Specified: boolean;
    FPURPOSE: string;
    FPURPOSE_Specified: boolean;
    FSTATUS: Integer;
    FADMIT_START: TXSDateTime;
    FADMIT_END: TXSDateTime;
    procedure SetPERSON_INFO(Index: Integer; const Astring: string);
    function  PERSON_INFO_Specified(Index: Integer): boolean;
    procedure SetPURPOSE(Index: Integer; const Astring: string);
    function  PURPOSE_Specified(Index: Integer): boolean;
  public
    destructor Destroy; override;
  published
    property ID:          guid         read FID write FID;
    property NUMBER:      Integer      read FNUMBER write FNUMBER;
    property DATE:        TXSDateTime  read FDATE write FDATE;
    property ORGUNIT_ID:  guid         read FORGUNIT_ID write FORGUNIT_ID;
    property PERSON_ID:   guid         read FPERSON_ID write FPERSON_ID;
    property PERSON_INFO: string       Index (IS_OPTN) read FPERSON_INFO write SetPERSON_INFO stored PERSON_INFO_Specified;
    property PURPOSE:     string       Index (IS_OPTN) read FPURPOSE write SetPURPOSE stored PURPOSE_Specified;
    property STATUS:      Integer      read FSTATUS write FSTATUS;
    property ADMIT_START: TXSDateTime  read FADMIT_START write FADMIT_START;
    property ADMIT_END:   TXSDateTime  read FADMIT_END write FADMIT_END;
  end;

  ArrayOfEvent = array of Event;                { "http://parsec.ru/Parsec3IntergationService"[GblCplx] }
  ArrayOfGuid = array of guid;                  { "http://parsec.ru/Parsec3IntergationService"[GblCplx] }


  // ************************************************************************ //
  // XML       : EventHistoryQueryParams, global, <complexType>
  // Namespace : http://parsec.ru/Parsec3IntergationService
  // ************************************************************************ //
  EventHistoryQueryParams = class(TRemotable)
  private
    FIDs: ArrayOfGuid;
    FIDs_Specified: boolean;
    FParentEventID: guid;
    FStartDate: TXSDateTime;
    FEndDate: TXSDateTime;
    FStartTime0: TXSDateTime;
    FEndTime0: TXSDateTime;
    FStartTime1: TXSDateTime;
    FEndTime1: TXSDateTime;
    FTerritories: ArrayOfGuid;
    FTerritories_Specified: boolean;
    FOperators: ArrayOfGuid;
    FOperators_Specified: boolean;
    FTransactionTypes: ArrayOfUnsignedInt;
    FTransactionTypes_Specified: boolean;
    FOrganizations: ArrayOfGuid;
    FOrganizations_Specified: boolean;
    FUsers: ArrayOfGuid;
    FUsers_Specified: boolean;
    FEventsWithoutUser: Boolean;
    FMaxResultSize: Integer;
    procedure SetIDs(Index: Integer; const AArrayOfGuid: ArrayOfGuid);
    function  IDs_Specified(Index: Integer): boolean;
    procedure SetTerritories(Index: Integer; const AArrayOfGuid: ArrayOfGuid);
    function  Territories_Specified(Index: Integer): boolean;
    procedure SetOperators(Index: Integer; const AArrayOfGuid: ArrayOfGuid);
    function  Operators_Specified(Index: Integer): boolean;
    procedure SetTransactionTypes(Index: Integer; const AArrayOfUnsignedInt: ArrayOfUnsignedInt);
    function  TransactionTypes_Specified(Index: Integer): boolean;
    procedure SetOrganizations(Index: Integer; const AArrayOfGuid: ArrayOfGuid);
    function  Organizations_Specified(Index: Integer): boolean;
    procedure SetUsers(Index: Integer; const AArrayOfGuid: ArrayOfGuid);
    function  Users_Specified(Index: Integer): boolean;
  public
    destructor Destroy; override;
  published
    property IDs:               ArrayOfGuid         Index (IS_OPTN) read FIDs write SetIDs stored IDs_Specified;
    property ParentEventID:     guid                Index (IS_NLBL) read FParentEventID write FParentEventID;
    property StartDate:         TXSDateTime         Index (IS_NLBL) read FStartDate write FStartDate;
    property EndDate:           TXSDateTime         Index (IS_NLBL) read FEndDate write FEndDate;
    property StartTime0:        TXSDateTime         Index (IS_NLBL) read FStartTime0 write FStartTime0;
    property EndTime0:          TXSDateTime         Index (IS_NLBL) read FEndTime0 write FEndTime0;
    property StartTime1:        TXSDateTime         Index (IS_NLBL) read FStartTime1 write FStartTime1;
    property EndTime1:          TXSDateTime         Index (IS_NLBL) read FEndTime1 write FEndTime1;
    property Territories:       ArrayOfGuid         Index (IS_OPTN) read FTerritories write SetTerritories stored Territories_Specified;
    property Operators:         ArrayOfGuid         Index (IS_OPTN) read FOperators write SetOperators stored Operators_Specified;
    property TransactionTypes:  ArrayOfUnsignedInt  Index (IS_OPTN) read FTransactionTypes write SetTransactionTypes stored TransactionTypes_Specified;
    property Organizations:     ArrayOfGuid         Index (IS_OPTN) read FOrganizations write SetOrganizations stored Organizations_Specified;
    property Users:             ArrayOfGuid         Index (IS_OPTN) read FUsers write SetUsers stored Users_Specified;
    property EventsWithoutUser: Boolean             Index (IS_NLBL) read FEventsWithoutUser write FEventsWithoutUser;
    property MaxResultSize:     Integer             Index (IS_NLBL) read FMaxResultSize write FMaxResultSize;
  end;

  ArrayOfString = array of string;              { "http://parsec.ru/Parsec3IntergationService"[GblCplx] }


  // ************************************************************************ //
  // XML       : EventsHistory, global, <complexType>
  // Namespace : http://parsec.ru/Parsec3IntergationService
  // ************************************************************************ //
  EventsHistory = class(TRemotable)
  private
    FEvents: ArrayOfEvent;
    FEvents_Specified: boolean;
    FPersons: ArrayOfGuid;
    FPersons_Specified: boolean;
    FPersonFullNames: ArrayOfString;
    FPersonFullNames_Specified: boolean;
    FTerritories: ArrayOfGuid;
    FTerritories_Specified: boolean;
    FTerritoryNames: ArrayOfString;
    FTerritoryNames_Specified: boolean;
    procedure SetEvents(Index: Integer; const AArrayOfEvent: ArrayOfEvent);
    function  Events_Specified(Index: Integer): boolean;
    procedure SetPersons(Index: Integer; const AArrayOfGuid: ArrayOfGuid);
    function  Persons_Specified(Index: Integer): boolean;
    procedure SetPersonFullNames(Index: Integer; const AArrayOfString: ArrayOfString);
    function  PersonFullNames_Specified(Index: Integer): boolean;
    procedure SetTerritories(Index: Integer; const AArrayOfGuid: ArrayOfGuid);
    function  Territories_Specified(Index: Integer): boolean;
    procedure SetTerritoryNames(Index: Integer; const AArrayOfString: ArrayOfString);
    function  TerritoryNames_Specified(Index: Integer): boolean;
  public
    destructor Destroy; override;
  published
    property Events:          ArrayOfEvent   Index (IS_OPTN) read FEvents write SetEvents stored Events_Specified;
    property Persons:         ArrayOfGuid    Index (IS_OPTN) read FPersons write SetPersons stored Persons_Specified;
    property PersonFullNames: ArrayOfString  Index (IS_OPTN) read FPersonFullNames write SetPersonFullNames stored PersonFullNames_Specified;
    property Territories:     ArrayOfGuid    Index (IS_OPTN) read FTerritories write SetTerritories stored Territories_Specified;
    property TerritoryNames:  ArrayOfString  Index (IS_OPTN) read FTerritoryNames write SetTerritoryNames stored TerritoryNames_Specified;
  end;



  // ************************************************************************ //
  // XML       : BaseTerritory, global, <complexType>
  // Namespace : http://parsec.ru/Parsec3IntergationService
  // ************************************************************************ //
  BaseTerritory = class(TRemotable)
  private
    FID: guid;
    FTYPE_: Byte;
    FNAME_: string;
    FNAME__Specified: boolean;
    FDESC: string;
    FDESC_Specified: boolean;
    procedure SetNAME_(Index: Integer; const Astring: string);
    function  NAME__Specified(Index: Integer): boolean;
    procedure SetDESC(Index: Integer; const Astring: string);
    function  DESC_Specified(Index: Integer): boolean;
  published
    property ID:    guid    read FID write FID;
    property TYPE_: Byte    read FTYPE_ write FTYPE_;
    property NAME_: string  Index (IS_OPTN) read FNAME_ write SetNAME_ stored NAME__Specified;
    property DESC:  string  Index (IS_OPTN) read FDESC write SetDESC stored DESC_Specified;
  end;



  // ************************************************************************ //
  // XML       : Territory, global, <complexType>
  // Namespace : http://parsec.ru/Parsec3IntergationService
  // ************************************************************************ //
  Territory = class(BaseTerritory)
  private
    FPARENT_ID: guid;
  published
    property PARENT_ID: guid  read FPARENT_ID write FPARENT_ID;
  end;

  ArrayOfBaseTerritory = array of BaseTerritory;   { "http://parsec.ru/Parsec3IntergationService"[GblCplx] }


  // ************************************************************************ //
  // XML       : EventsHistoryResult, global, <complexType>
  // Namespace : http://parsec.ru/Parsec3IntergationService
  // ************************************************************************ //
  EventsHistoryResult = class(BaseResult)
  private
    FValue: EventsHistory;
    FValue_Specified: boolean;
    procedure SetValue(Index: Integer; const AEventsHistory: EventsHistory);
    function  Value_Specified(Index: Integer): boolean;
  public
    destructor Destroy; override;
  published
    property Value: EventsHistory  Index (IS_OPTN) read FValue write SetValue stored Value_Specified;
  end;



  // ************************************************************************ //
  // XML       : StringResult, global, <complexType>
  // Namespace : http://parsec.ru/Parsec3IntergationService
  // ************************************************************************ //
  StringResult = class(BaseResult)
  private
    FValue: string;
    FValue_Specified: boolean;
    procedure SetValue(Index: Integer; const Astring: string);
    function  Value_Specified(Index: Integer): boolean;
  published
    property Value: string  Index (IS_OPTN) read FValue write SetValue stored Value_Specified;
  end;

  ArrayOfExtraFieldValue = array of ExtraFieldValue;   { "http://parsec.ru/Parsec3IntergationService"[GblCplx] }


  // ************************************************************************ //
  // XML       : ExtraFieldValue, global, <complexType>
  // Namespace : http://parsec.ru/Parsec3IntergationService
  // ************************************************************************ //
  ExtraFieldValue = class(BaseObject)
  private
    FTEMPLATE_ID: guid;
    FVALUE: string;
    FVALUE_Specified: boolean;
    procedure SetVALUE(Index: Integer; const Astring: string);
    function  VALUE_Specified(Index: Integer): boolean;
  published
    property TEMPLATE_ID: guid    read FTEMPLATE_ID write FTEMPLATE_ID;
    property VALUE:       string  Index (IS_OPTN) read FVALUE write SetVALUE stored VALUE_Specified;
  end;



  // ************************************************************************ //
  // XML       : ObjectResult, global, <complexType>
  // Namespace : http://parsec.ru/Parsec3IntergationService
  // ************************************************************************ //
  ObjectResult = class(BaseResult)
  private
    FValue: string;
    FValue_Specified: boolean;
    procedure SetValue(Index: Integer; const Astring: string);
    function  Value_Specified(Index: Integer): boolean;
  published
    property Value: string  Index (IS_OPTN) read FValue write SetValue stored Value_Specified;
  end;

  ArrayOfPersonExtraFieldTemplate = array of PersonExtraFieldTemplate;   { "http://parsec.ru/Parsec3IntergationService"[GblCplx] }


  // ************************************************************************ //
  // XML       : PersonExtraFieldTemplate, global, <complexType>
  // Namespace : http://parsec.ru/Parsec3IntergationService
  // ************************************************************************ //
  PersonExtraFieldTemplate = class(TRemotable)
  private
    FID: guid;
    FTYPE_: XmlTypeCode;
    FNAME_: string;
    FNAME__Specified: boolean;
    FGROUP_NAME: string;
    FGROUP_NAME_Specified: boolean;
    FIS_BASE: Boolean;
    FORDER_INDEX: Integer;
    procedure SetNAME_(Index: Integer; const Astring: string);
    function  NAME__Specified(Index: Integer): boolean;
    procedure SetGROUP_NAME(Index: Integer; const Astring: string);
    function  GROUP_NAME_Specified(Index: Integer): boolean;
  published
    property ID:          guid         read FID write FID;
    property TYPE_:       XmlTypeCode  read FTYPE_ write FTYPE_;
    property NAME_:       string       Index (IS_OPTN) read FNAME_ write SetNAME_ stored NAME__Specified;
    property GROUP_NAME:  string       Index (IS_OPTN) read FGROUP_NAME write SetGROUP_NAME stored GROUP_NAME_Specified;
    property IS_BASE:     Boolean      read FIS_BASE write FIS_BASE;
    property ORDER_INDEX: Integer      read FORDER_INDEX write FORDER_INDEX;
  end;


  // ************************************************************************ //
  // Namespace : http://parsec.ru/Parsec3IntergationService
  // soapAction: http://parsec.ru/Parsec3IntergationService/%operationName%
  // transport : http://schemas.xmlsoap.org/soap/http
  // style     : document
  // use       : literal
  // binding   : IntegrationServiceSoap12
  // service   : IntegrationService
  // port      : IntegrationServiceSoap12
  // URL       : http://127.0.0.1:10101/IntegrationService/IntegrationService.asmx
  // ************************************************************************ //
  IntegrationServiceSoap = interface(IInvokable)
  ['{5716FECA-FD56-1257-DB15-2248E55507CD}']
    function  GetVersion: string; stdcall;
    function  OpenSession(const domain: string; const userName: string; const password: string): SessionResult; stdcall;
    function  OpenSessionWithInLocale(const domain: string; const userName: string; const password: string; const locale: string): SessionResult; stdcall;
    function  ContinueSession(const sessionID: guid): Integer; stdcall;
    procedure CloseSession(const sessionID: guid); stdcall;
    function  GetOrgUnit(const sessionID: guid; const orgUnitID: guid): OrgUnit; stdcall;
    function  GetOrgUnitSubItems(const sessionID: guid; const orgUnitID: guid): ArrayOfBaseObject; stdcall;
    function  GetOrgUnitsHierarhy(const sessionID: guid): ArrayOfOrgUnit; stdcall;
    function  GetOrgUnitsHierarhyWithPersons(const sessionID: guid): ArrayOfBaseObject; stdcall;
    function  GetOrgUnitsHierarhyWithVisitors(const sessionID: guid): ArrayOfBaseObject; stdcall;
    function  GetOrgUnitSubItemsHierarhyWithPersons(const sessionID: guid; const orgUnitID: guid): ArrayOfBaseObject; stdcall;
    function  GetOrgUnitSubItemsHierarhyWithVisitors(const sessionID: guid; const orgUnitID: guid): ArrayOfBaseObject; stdcall;
    function  GetPerson(const sessionID: guid; const personID: guid): PersonWithPhoto; stdcall;
    function  GetAccessGroups(const sessionID: guid): ArrayOfAccessGroup; stdcall;
    function  GetPersonIdentifiers(const sessionID: guid; const personID: guid): ArrayOfIdentifier; stdcall;
    function  CreatePerson(const sessionID: guid; const person: Person): GuidResult; stdcall;
    function  CreateVisitor(const sessionID: guid; const person: Person): GuidResult; stdcall;
    function  OpenPersonEditingSession(const sessionID: guid; const personID: guid): GuidResult; stdcall;
    procedure ClosePersonEditingSession(const personEditSessionID: guid); stdcall;
    function  SavePerson(const personEditSessionID: guid; const person: BasePerson): BaseResult; stdcall;
    function  SetPersonPhoto(const personEditSessionID: guid; const photoByteArray: TByteDynArray): BaseResult; stdcall;
    function  SetPersonOrgUnit(const personEditSessionID: guid; const orgUnitID: guid): BaseResult; stdcall;
    function  DeletePerson(const sessionID: guid; const personID: guid): BaseResult; stdcall;
    function  CreateOrgUnit(const sessionID: guid; const orgUnit: OrgUnit): GuidResult; stdcall;
    function  OpenOrgUnitEditingSession(const sessionID: guid; const orgUnitID: guid): GuidResult; stdcall;
    procedure CloseOrgUnitEditingSession(const orgUnitEditSessionID: guid); stdcall;
    function  SaveOrgUnit(const orgUnitEditSessionID: guid; const orgUnit: BaseOrgUnit): BaseResult; stdcall;
    function  DeleteOrgUnit(const sessionID: guid; const orgUnitID: guid): BaseResult; stdcall;
    function  DeleteIdentifier(const sessionID: guid; const Code: string): BaseResult; stdcall;
    function  AddPersonIdentifier(const personEditSessionID: guid; const identifier: BaseIdentifier): BaseResult; stdcall;
    function  ChangePersonIdentifier(const personEditSessionID: guid; const identifier: BaseIdentifier): BaseResult; stdcall;
    function  GetTerritoriesHierarhy(const sessionID: guid): ArrayOfTerritory; stdcall;
    function  GetTerritorySubItems(const sessionID: guid; const TerraID: guid): ArrayOfBaseTerritory; stdcall;
    function  GetEvents(const sessionID: guid; const TerritoryID: guid; const PersNodeID: guid; const dtFrom: TXSDateTime; const dtTo: TXSDateTime): EventsHistoryResult; stdcall;
    function  GetPersonExtraFieldTemplates(const sessionID: guid): ArrayOfPersonExtraFieldTemplate; stdcall;
    function  GetPersonExtraFieldValue(const sessionID: guid; const personID: guid; const templateID: guid): ObjectResult; stdcall;
    function  GetPersonExtraFieldValueString(const sessionID: guid; const personID: guid; const templateID: guid): StringResult; stdcall;
    function  SetPersonExtraFieldValue(const personEditSessionID: guid; const templateID: guid; const value: string): BaseResult; stdcall;
    function  GetPersonExtraFieldValues(const sessionID: guid; const personID: guid): ArrayOfExtraFieldValue; stdcall;
    function  SetPersonExtraFieldValues(const personEditSessionID: guid; const values: ArrayOfExtraFieldValue): BaseResult; stdcall;
    function  ValidateExtraFieldValue(const sessionID: guid; const value: ExtraFieldValue): BaseResult; stdcall;
    function  FindVisitorRequest(const sessionID: guid; const requestNumber: Integer): VisitorRequest; stdcall;
    function  GetAcceptedVisitorRequests(const sessionID: guid): ArrayOfVisitorRequest; stdcall;
    function  GetPersonVisitorRequests(const sessionID: guid; const visitorID: guid; const issued: Boolean; const accepted: Boolean; const declined: Boolean; const active: Boolean;
                                       const completed: Boolean): ArrayOfVisitorRequest; stdcall;
    function  ActivateVisitorRequest(const sessionID: guid; const requestID: guid; const cardCode: string): BaseResult; stdcall;
    function  CloseVisitorRequest(const sessionID: guid; const requestID: guid): BaseResult; stdcall;
    function  CloseAllActiveVisitorRequests(const sessionID: guid; const visitorID: guid): BaseResult; stdcall;
    function  FindPeople(const sessionID: guid; const lastname: string; const firstname: string; const middlename: string): ArrayOfPerson; stdcall;
    function  FindVisitors(const sessionID: guid; const lastname: string; const firstname: string; const middlename: string): ArrayOfPerson; stdcall;
    function  GetAccessSchedules(const sessionID: guid): ArrayOfSchedule; stdcall;
    function  GetScheduleIntervals(const sessionID: guid; const scheduleID: guid; const from: TXSDateTime; const to_: TXSDateTime): ArrayOfTimeInterval; stdcall;
    function  CreateTempAccessGroup(const sessionID: guid; const scheduleID: guid; const territories: ArrayOfGuid): GuidResult; stdcall;
    function  GetRootTerritory(const sessionID: guid): Territory; stdcall;
    function  GetTerritory(const sessionID: guid; const territoryID: guid): Territory; stdcall;
    function  GetRootOrgUnit(const sessionID: guid): OrgUnit; stdcall;
    function  GetDomains: ArrayOfDomain; stdcall;
    function  CreateVisitorRequest(const sessionID: guid; const request: VisitorRequest): VisitorRequest; stdcall;
    function  DeleteIssuedVisitorRequest(const sessionID: guid; const requestID: guid): BaseResult; stdcall;
    function  GetIssuedVisitorRequests(const sessionID: guid): ArrayOfVisitorRequest; stdcall;
    function  GetVisitorRequest(const sessionID: guid; const requestID: guid): VisitorRequest; stdcall;
    function  SaveVisitorRequest(const sessionID: guid; const request: VisitorRequest): BaseResult; stdcall;
    function  OpenEventHistorySession(const sessionID: guid; const parameters: EventHistoryQueryParams): GuidResult; stdcall;
    procedure CloseEventHistorySession(const sessionID: guid; const eventHistorySessionID: guid); stdcall;
    function  GetEventHistoryResultCount(const sessionID: guid; const eventHistorySessionID: guid): Integer; stdcall;
    function  GetEventHistoryResult(const sessionID: guid; const eventHistorySessionID: guid; const fields: ArrayOfGuid; const offset: Integer; const count: Integer): ArrayOfEventObject; stdcall;
    function  FindPersonByIdentifier(const sessionID: guid; const cardCode: string): Person; stdcall;
    function  BlockPerson(const personEditSessionID: guid; const typeBlock: Integer): BaseResult; stdcall;
    function  UnblockPerson(const personEditSessionID: guid): BaseResult; stdcall;
  end;

function GetIntegrationServiceSoap(UseWSDL: Boolean=System.False; Addr: string=''; HTTPRIO: THTTPRIO = nil): IntegrationServiceSoap;


implementation
  uses SysUtils;

function GetIntegrationServiceSoap(UseWSDL: Boolean; Addr: string; HTTPRIO: THTTPRIO): IntegrationServiceSoap;
const
  defWSDL = 'C:\IntegrationService.asmx.xml';
  defURL  = 'http://127.0.0.1:10101/IntegrationService/IntegrationService.asmx';
  defSvc  = 'IntegrationService';
  defPrt  = 'IntegrationServiceSoap12';
var
  RIO: THTTPRIO;
begin
  Result := nil;
  if (Addr = '') then
  begin
    if UseWSDL then
      Addr := defWSDL
    else
      Addr := defURL;
  end;
  if HTTPRIO = nil then
    RIO := THTTPRIO.Create(nil)
  else
    RIO := HTTPRIO;
  try
    Result := (RIO as IntegrationServiceSoap);
    if UseWSDL then
    begin
      RIO.WSDLLocation := Addr;
      RIO.Service := defSvc;
      RIO.Port := defPrt;
    end else
      RIO.URL := Addr;
  finally
    if (Result = nil) and (HTTPRIO = nil) then
      RIO.Free;
  end;
end;


procedure AccessGroup.SetNAME_(Index: Integer; const Astring: string);
begin
  FNAME_ := Astring;
  FNAME__Specified := True;
end;

function AccessGroup.NAME__Specified(Index: Integer): boolean;
begin
  Result := FNAME__Specified;
end;

procedure BaseResult.SetErrorMessage(Index: Integer; const Astring: string);
begin
  FErrorMessage := Astring;
  FErrorMessage_Specified := True;
end;

function BaseResult.ErrorMessage_Specified(Index: Integer): boolean;
begin
  Result := FErrorMessage_Specified;
end;

destructor SessionResult.Destroy;
begin
  SysUtils.FreeAndNil(FValue);
  inherited Destroy;
end;

procedure SessionResult.SetValue(Index: Integer; const ASession: Session);
begin
  FValue := ASession;
  FValue_Specified := True;
end;

function SessionResult.Value_Specified(Index: Integer): boolean;
begin
  Result := FValue_Specified;
end;

procedure BaseOrgUnit.SetNAME_(Index: Integer; const Astring: string);
begin
  FNAME_ := Astring;
  FNAME__Specified := True;
end;

function BaseOrgUnit.NAME__Specified(Index: Integer): boolean;
begin
  Result := FNAME__Specified;
end;

procedure BaseOrgUnit.SetDESC(Index: Integer; const Astring: string);
begin
  FDESC := Astring;
  FDESC_Specified := True;
end;

function BaseOrgUnit.DESC_Specified(Index: Integer): boolean;
begin
  Result := FDESC_Specified;
end;

procedure BaseIdentifier.SetCODE(Index: Integer; const Astring: string);
begin
  FCODE := Astring;
  FCODE_Specified := True;
end;

function BaseIdentifier.CODE_Specified(Index: Integer): boolean;
begin
  Result := FCODE_Specified;
end;

destructor IdentifierTemp.Destroy;
begin
  SysUtils.FreeAndNil(FVALID_FROM);
  SysUtils.FreeAndNil(FVALID_TO);
  inherited Destroy;
end;

procedure BasePerson.SetFIRST_NAME(Index: Integer; const Astring: string);
begin
  FFIRST_NAME := Astring;
  FFIRST_NAME_Specified := True;
end;

function BasePerson.FIRST_NAME_Specified(Index: Integer): boolean;
begin
  Result := FFIRST_NAME_Specified;
end;

procedure BasePerson.SetLAST_NAME(Index: Integer; const Astring: string);
begin
  FLAST_NAME := Astring;
  FLAST_NAME_Specified := True;
end;

function BasePerson.LAST_NAME_Specified(Index: Integer): boolean;
begin
  Result := FLAST_NAME_Specified;
end;

procedure BasePerson.SetMIDDLE_NAME(Index: Integer; const Astring: string);
begin
  FMIDDLE_NAME := Astring;
  FMIDDLE_NAME_Specified := True;
end;

function BasePerson.MIDDLE_NAME_Specified(Index: Integer): boolean;
begin
  Result := FMIDDLE_NAME_Specified;
end;

procedure BasePerson.SetTAB_NUM(Index: Integer; const Astring: string);
begin
  FTAB_NUM := Astring;
  FTAB_NUM_Specified := True;
end;

function BasePerson.TAB_NUM_Specified(Index: Integer): boolean;
begin
  Result := FTAB_NUM_Specified;
end;

procedure PersonWithPhoto.SetPHOTO(Index: Integer; const ATByteDynArray: TByteDynArray);
begin
  FPHOTO := ATByteDynArray;
  FPHOTO_Specified := True;
end;

function PersonWithPhoto.PHOTO_Specified(Index: Integer): boolean;
begin
  Result := FPHOTO_Specified;
end;

destructor Event.Destroy;
begin
  SysUtils.FreeAndNil(FEventDate);
  inherited Destroy;
end;

procedure Event.SetCODE(Index: Integer; const Astring: string);
begin
  FCODE := Astring;
  FCODE_Specified := True;
end;

function Event.CODE_Specified(Index: Integer): boolean;
begin
  Result := FCODE_Specified;
end;

destructor TimeInterval.Destroy;
begin
  SysUtils.FreeAndNil(FSTART);
  SysUtils.FreeAndNil(FEND_);
  inherited Destroy;
end;

procedure Schedule.SetNAME_(Index: Integer; const Astring: string);
begin
  FNAME_ := Astring;
  FNAME__Specified := True;
end;

function Schedule.NAME__Specified(Index: Integer): boolean;
begin
  Result := FNAME__Specified;
end;

procedure EventObject.SetValues(Index: Integer; const AArrayOfAnyType: ArrayOfAnyType);
begin
  FValues := AArrayOfAnyType;
  FValues_Specified := True;
end;

function EventObject.Values_Specified(Index: Integer): boolean;
begin
  Result := FValues_Specified;
end;

procedure Domain.SetNAME_(Index: Integer; const Astring: string);
begin
  FNAME_ := Astring;
  FNAME__Specified := True;
end;

function Domain.NAME__Specified(Index: Integer): boolean;
begin
  Result := FNAME__Specified;
end;

procedure Domain.SetDESCRIPTION(Index: Integer; const Astring: string);
begin
  FDESCRIPTION := Astring;
  FDESCRIPTION_Specified := True;
end;

function Domain.DESCRIPTION_Specified(Index: Integer): boolean;
begin
  Result := FDESCRIPTION_Specified;
end;

destructor VisitorRequest.Destroy;
begin
  SysUtils.FreeAndNil(FDATE);
  SysUtils.FreeAndNil(FADMIT_START);
  SysUtils.FreeAndNil(FADMIT_END);
  inherited Destroy;
end;

procedure VisitorRequest.SetPERSON_INFO(Index: Integer; const Astring: string);
begin
  FPERSON_INFO := Astring;
  FPERSON_INFO_Specified := True;
end;

function VisitorRequest.PERSON_INFO_Specified(Index: Integer): boolean;
begin
  Result := FPERSON_INFO_Specified;
end;

procedure VisitorRequest.SetPURPOSE(Index: Integer; const Astring: string);
begin
  FPURPOSE := Astring;
  FPURPOSE_Specified := True;
end;

function VisitorRequest.PURPOSE_Specified(Index: Integer): boolean;
begin
  Result := FPURPOSE_Specified;
end;

destructor EventHistoryQueryParams.Destroy;
begin
  SysUtils.FreeAndNil(FStartDate);
  SysUtils.FreeAndNil(FEndDate);
  SysUtils.FreeAndNil(FStartTime0);
  SysUtils.FreeAndNil(FEndTime0);
  SysUtils.FreeAndNil(FStartTime1);
  SysUtils.FreeAndNil(FEndTime1);
  inherited Destroy;
end;

procedure EventHistoryQueryParams.SetIDs(Index: Integer; const AArrayOfGuid: ArrayOfGuid);
begin
  FIDs := AArrayOfGuid;
  FIDs_Specified := True;
end;

function EventHistoryQueryParams.IDs_Specified(Index: Integer): boolean;
begin
  Result := FIDs_Specified;
end;

procedure EventHistoryQueryParams.SetTerritories(Index: Integer; const AArrayOfGuid: ArrayOfGuid);
begin
  FTerritories := AArrayOfGuid;
  FTerritories_Specified := True;
end;

function EventHistoryQueryParams.Territories_Specified(Index: Integer): boolean;
begin
  Result := FTerritories_Specified;
end;

procedure EventHistoryQueryParams.SetOperators(Index: Integer; const AArrayOfGuid: ArrayOfGuid);
begin
  FOperators := AArrayOfGuid;
  FOperators_Specified := True;
end;

function EventHistoryQueryParams.Operators_Specified(Index: Integer): boolean;
begin
  Result := FOperators_Specified;
end;

procedure EventHistoryQueryParams.SetTransactionTypes(Index: Integer; const AArrayOfUnsignedInt: ArrayOfUnsignedInt);
begin
  FTransactionTypes := AArrayOfUnsignedInt;
  FTransactionTypes_Specified := True;
end;

function EventHistoryQueryParams.TransactionTypes_Specified(Index: Integer): boolean;
begin
  Result := FTransactionTypes_Specified;
end;

procedure EventHistoryQueryParams.SetOrganizations(Index: Integer; const AArrayOfGuid: ArrayOfGuid);
begin
  FOrganizations := AArrayOfGuid;
  FOrganizations_Specified := True;
end;

function EventHistoryQueryParams.Organizations_Specified(Index: Integer): boolean;
begin
  Result := FOrganizations_Specified;
end;

procedure EventHistoryQueryParams.SetUsers(Index: Integer; const AArrayOfGuid: ArrayOfGuid);
begin
  FUsers := AArrayOfGuid;
  FUsers_Specified := True;
end;

function EventHistoryQueryParams.Users_Specified(Index: Integer): boolean;
begin
  Result := FUsers_Specified;
end;

destructor EventsHistory.Destroy;
var
  I: Integer;
begin
  for I := 0 to System.Length(FEvents)-1 do
    SysUtils.FreeAndNil(FEvents[I]);
  System.SetLength(FEvents, 0);
  inherited Destroy;
end;

procedure EventsHistory.SetEvents(Index: Integer; const AArrayOfEvent: ArrayOfEvent);
begin
  FEvents := AArrayOfEvent;
  FEvents_Specified := True;
end;

function EventsHistory.Events_Specified(Index: Integer): boolean;
begin
  Result := FEvents_Specified;
end;

procedure EventsHistory.SetPersons(Index: Integer; const AArrayOfGuid: ArrayOfGuid);
begin
  FPersons := AArrayOfGuid;
  FPersons_Specified := True;
end;

function EventsHistory.Persons_Specified(Index: Integer): boolean;
begin
  Result := FPersons_Specified;
end;

procedure EventsHistory.SetPersonFullNames(Index: Integer; const AArrayOfString: ArrayOfString);
begin
  FPersonFullNames := AArrayOfString;
  FPersonFullNames_Specified := True;
end;

function EventsHistory.PersonFullNames_Specified(Index: Integer): boolean;
begin
  Result := FPersonFullNames_Specified;
end;

procedure EventsHistory.SetTerritories(Index: Integer; const AArrayOfGuid: ArrayOfGuid);
begin
  FTerritories := AArrayOfGuid;
  FTerritories_Specified := True;
end;

function EventsHistory.Territories_Specified(Index: Integer): boolean;
begin
  Result := FTerritories_Specified;
end;

procedure EventsHistory.SetTerritoryNames(Index: Integer; const AArrayOfString: ArrayOfString);
begin
  FTerritoryNames := AArrayOfString;
  FTerritoryNames_Specified := True;
end;

function EventsHistory.TerritoryNames_Specified(Index: Integer): boolean;
begin
  Result := FTerritoryNames_Specified;
end;

procedure BaseTerritory.SetNAME_(Index: Integer; const Astring: string);
begin
  FNAME_ := Astring;
  FNAME__Specified := True;
end;

function BaseTerritory.NAME__Specified(Index: Integer): boolean;
begin
  Result := FNAME__Specified;
end;

procedure BaseTerritory.SetDESC(Index: Integer; const Astring: string);
begin
  FDESC := Astring;
  FDESC_Specified := True;
end;

function BaseTerritory.DESC_Specified(Index: Integer): boolean;
begin
  Result := FDESC_Specified;
end;

destructor EventsHistoryResult.Destroy;
begin
  SysUtils.FreeAndNil(FValue);
  inherited Destroy;
end;

procedure EventsHistoryResult.SetValue(Index: Integer; const AEventsHistory: EventsHistory);
begin
  FValue := AEventsHistory;
  FValue_Specified := True;
end;

function EventsHistoryResult.Value_Specified(Index: Integer): boolean;
begin
  Result := FValue_Specified;
end;

procedure StringResult.SetValue(Index: Integer; const Astring: string);
begin
  FValue := Astring;
  FValue_Specified := True;
end;

function StringResult.Value_Specified(Index: Integer): boolean;
begin
  Result := FValue_Specified;
end;

procedure ExtraFieldValue.SetVALUE(Index: Integer; const Astring: string);
begin
  FVALUE := Astring;
  FVALUE_Specified := True;
end;

function ExtraFieldValue.VALUE_Specified(Index: Integer): boolean;
begin
  Result := FVALUE_Specified;
end;

procedure ObjectResult.SetValue(Index: Integer; const Astring: string);
begin
  FValue := Astring;
  FValue_Specified := True;
end;

function ObjectResult.Value_Specified(Index: Integer): boolean;
begin
  Result := FValue_Specified;
end;

procedure PersonExtraFieldTemplate.SetNAME_(Index: Integer; const Astring: string);
begin
  FNAME_ := Astring;
  FNAME__Specified := True;
end;

function PersonExtraFieldTemplate.NAME__Specified(Index: Integer): boolean;
begin
  Result := FNAME__Specified;
end;

procedure PersonExtraFieldTemplate.SetGROUP_NAME(Index: Integer; const Astring: string);
begin
  FGROUP_NAME := Astring;
  FGROUP_NAME_Specified := True;
end;

function PersonExtraFieldTemplate.GROUP_NAME_Specified(Index: Integer): boolean;
begin
  Result := FGROUP_NAME_Specified;
end;

initialization
  { IntegrationServiceSoap }
  InvRegistry.RegisterInterface(TypeInfo(IntegrationServiceSoap), 'http://parsec.ru/Parsec3IntergationService', 'utf-8');
  InvRegistry.RegisterDefaultSOAPAction(TypeInfo(IntegrationServiceSoap), 'http://parsec.ru/Parsec3IntergationService/%operationName%');
  InvRegistry.RegisterInvokeOptions(TypeInfo(IntegrationServiceSoap), ioDocument);
  InvRegistry.RegisterInvokeOptions(TypeInfo(IntegrationServiceSoap), ioSOAP12);
  { IntegrationServiceSoap.GetVersion }
  InvRegistry.RegisterMethodInfo(TypeInfo(IntegrationServiceSoap), 'GetVersion', '',
                                 '[ReturnName="GetVersionResult"]', IS_OPTN);
  { IntegrationServiceSoap.OpenSession }
  InvRegistry.RegisterMethodInfo(TypeInfo(IntegrationServiceSoap), 'OpenSession', '',
                                 '[ReturnName="OpenSessionResult"]', IS_OPTN);
  { IntegrationServiceSoap.OpenSessionWithInLocale }
  InvRegistry.RegisterMethodInfo(TypeInfo(IntegrationServiceSoap), 'OpenSessionWithInLocale', '',
                                 '[ReturnName="OpenSessionWithInLocaleResult"]', IS_OPTN);
  { IntegrationServiceSoap.ContinueSession }
  InvRegistry.RegisterMethodInfo(TypeInfo(IntegrationServiceSoap), 'ContinueSession', '',
                                 '[ReturnName="ContinueSessionResult"]');
  InvRegistry.RegisterParamInfo(TypeInfo(IntegrationServiceSoap), 'ContinueSession', 'sessionID', '',
                                '[Namespace="http://microsoft.com/wsdl/types/"]');
  { IntegrationServiceSoap.CloseSession }
  InvRegistry.RegisterParamInfo(TypeInfo(IntegrationServiceSoap), 'CloseSession', 'sessionID', '',
                                '[Namespace="http://microsoft.com/wsdl/types/"]');
  { IntegrationServiceSoap.GetOrgUnit }
  InvRegistry.RegisterMethodInfo(TypeInfo(IntegrationServiceSoap), 'GetOrgUnit', '',
                                 '[ReturnName="GetOrgUnitResult"]', IS_OPTN);
  InvRegistry.RegisterParamInfo(TypeInfo(IntegrationServiceSoap), 'GetOrgUnit', 'sessionID', '',
                                '[Namespace="http://microsoft.com/wsdl/types/"]');
  InvRegistry.RegisterParamInfo(TypeInfo(IntegrationServiceSoap), 'GetOrgUnit', 'orgUnitID', '',
                                '[Namespace="http://microsoft.com/wsdl/types/"]');
  { IntegrationServiceSoap.GetOrgUnitSubItems }
  InvRegistry.RegisterMethodInfo(TypeInfo(IntegrationServiceSoap), 'GetOrgUnitSubItems', '',
                                 '[ReturnName="GetOrgUnitSubItemsResult"]', IS_OPTN);
  InvRegistry.RegisterParamInfo(TypeInfo(IntegrationServiceSoap), 'GetOrgUnitSubItems', 'sessionID', '',
                                '[Namespace="http://microsoft.com/wsdl/types/"]');
  InvRegistry.RegisterParamInfo(TypeInfo(IntegrationServiceSoap), 'GetOrgUnitSubItems', 'orgUnitID', '',
                                '[Namespace="http://microsoft.com/wsdl/types/"]');
  InvRegistry.RegisterParamInfo(TypeInfo(IntegrationServiceSoap), 'GetOrgUnitSubItems', 'GetOrgUnitSubItemsResult', '',
                                '[ArrayItemName="BaseObject"]');
  { IntegrationServiceSoap.GetOrgUnitsHierarhy }
  InvRegistry.RegisterMethodInfo(TypeInfo(IntegrationServiceSoap), 'GetOrgUnitsHierarhy', '',
                                 '[ReturnName="GetOrgUnitsHierarhyResult"]', IS_OPTN);
  InvRegistry.RegisterParamInfo(TypeInfo(IntegrationServiceSoap), 'GetOrgUnitsHierarhy', 'sessionID', '',
                                '[Namespace="http://microsoft.com/wsdl/types/"]');
  InvRegistry.RegisterParamInfo(TypeInfo(IntegrationServiceSoap), 'GetOrgUnitsHierarhy', 'GetOrgUnitsHierarhyResult', '',
                                '[ArrayItemName="OrgUnit"]');
  { IntegrationServiceSoap.GetOrgUnitsHierarhyWithPersons }
  InvRegistry.RegisterMethodInfo(TypeInfo(IntegrationServiceSoap), 'GetOrgUnitsHierarhyWithPersons', '',
                                 '[ReturnName="GetOrgUnitsHierarhyWithPersonsResult"]', IS_OPTN);
  InvRegistry.RegisterParamInfo(TypeInfo(IntegrationServiceSoap), 'GetOrgUnitsHierarhyWithPersons', 'sessionID', '',
                                '[Namespace="http://microsoft.com/wsdl/types/"]');
  InvRegistry.RegisterParamInfo(TypeInfo(IntegrationServiceSoap), 'GetOrgUnitsHierarhyWithPersons', 'GetOrgUnitsHierarhyWithPersonsResult', '',
                                '[ArrayItemName="BaseObject"]');
  { IntegrationServiceSoap.GetOrgUnitsHierarhyWithVisitors }
  InvRegistry.RegisterMethodInfo(TypeInfo(IntegrationServiceSoap), 'GetOrgUnitsHierarhyWithVisitors', '',
                                 '[ReturnName="GetOrgUnitsHierarhyWithVisitorsResult"]', IS_OPTN);
  InvRegistry.RegisterParamInfo(TypeInfo(IntegrationServiceSoap), 'GetOrgUnitsHierarhyWithVisitors', 'sessionID', '',
                                '[Namespace="http://microsoft.com/wsdl/types/"]');
  InvRegistry.RegisterParamInfo(TypeInfo(IntegrationServiceSoap), 'GetOrgUnitsHierarhyWithVisitors', 'GetOrgUnitsHierarhyWithVisitorsResult', '',
                                '[ArrayItemName="BaseObject"]');
  { IntegrationServiceSoap.GetOrgUnitSubItemsHierarhyWithPersons }
  InvRegistry.RegisterMethodInfo(TypeInfo(IntegrationServiceSoap), 'GetOrgUnitSubItemsHierarhyWithPersons', '',
                                 '[ReturnName="GetOrgUnitSubItemsHierarhyWithPersonsResult"]', IS_OPTN);
  InvRegistry.RegisterParamInfo(TypeInfo(IntegrationServiceSoap), 'GetOrgUnitSubItemsHierarhyWithPersons', 'sessionID', '',
                                '[Namespace="http://microsoft.com/wsdl/types/"]');
  InvRegistry.RegisterParamInfo(TypeInfo(IntegrationServiceSoap), 'GetOrgUnitSubItemsHierarhyWithPersons', 'orgUnitID', '',
                                '[Namespace="http://microsoft.com/wsdl/types/"]');
  InvRegistry.RegisterParamInfo(TypeInfo(IntegrationServiceSoap), 'GetOrgUnitSubItemsHierarhyWithPersons', 'GetOrgUnitSubItemsHierarhyWithPersonsResult', '',
                                '[ArrayItemName="BaseObject"]');
  { IntegrationServiceSoap.GetOrgUnitSubItemsHierarhyWithVisitors }
  InvRegistry.RegisterMethodInfo(TypeInfo(IntegrationServiceSoap), 'GetOrgUnitSubItemsHierarhyWithVisitors', '',
                                 '[ReturnName="GetOrgUnitSubItemsHierarhyWithVisitorsResult"]', IS_OPTN);
  InvRegistry.RegisterParamInfo(TypeInfo(IntegrationServiceSoap), 'GetOrgUnitSubItemsHierarhyWithVisitors', 'sessionID', '',
                                '[Namespace="http://microsoft.com/wsdl/types/"]');
  InvRegistry.RegisterParamInfo(TypeInfo(IntegrationServiceSoap), 'GetOrgUnitSubItemsHierarhyWithVisitors', 'orgUnitID', '',
                                '[Namespace="http://microsoft.com/wsdl/types/"]');
  InvRegistry.RegisterParamInfo(TypeInfo(IntegrationServiceSoap), 'GetOrgUnitSubItemsHierarhyWithVisitors', 'GetOrgUnitSubItemsHierarhyWithVisitorsResult', '',
                                '[ArrayItemName="BaseObject"]');
  { IntegrationServiceSoap.GetPerson }
  InvRegistry.RegisterMethodInfo(TypeInfo(IntegrationServiceSoap), 'GetPerson', '',
                                 '[ReturnName="GetPersonResult"]', IS_OPTN);
  InvRegistry.RegisterParamInfo(TypeInfo(IntegrationServiceSoap), 'GetPerson', 'sessionID', '',
                                '[Namespace="http://microsoft.com/wsdl/types/"]');
  InvRegistry.RegisterParamInfo(TypeInfo(IntegrationServiceSoap), 'GetPerson', 'personID', '',
                                '[Namespace="http://microsoft.com/wsdl/types/"]');
  { IntegrationServiceSoap.GetAccessGroups }
  InvRegistry.RegisterMethodInfo(TypeInfo(IntegrationServiceSoap), 'GetAccessGroups', '',
                                 '[ReturnName="GetAccessGroupsResult"]', IS_OPTN);
  InvRegistry.RegisterParamInfo(TypeInfo(IntegrationServiceSoap), 'GetAccessGroups', 'sessionID', '',
                                '[Namespace="http://microsoft.com/wsdl/types/"]');
  InvRegistry.RegisterParamInfo(TypeInfo(IntegrationServiceSoap), 'GetAccessGroups', 'GetAccessGroupsResult', '',
                                '[ArrayItemName="AccessGroup"]');
  { IntegrationServiceSoap.GetPersonIdentifiers }
  InvRegistry.RegisterMethodInfo(TypeInfo(IntegrationServiceSoap), 'GetPersonIdentifiers', '',
                                 '[ReturnName="GetPersonIdentifiersResult"]', IS_OPTN);
  InvRegistry.RegisterParamInfo(TypeInfo(IntegrationServiceSoap), 'GetPersonIdentifiers', 'sessionID', '',
                                '[Namespace="http://microsoft.com/wsdl/types/"]');
  InvRegistry.RegisterParamInfo(TypeInfo(IntegrationServiceSoap), 'GetPersonIdentifiers', 'personID', '',
                                '[Namespace="http://microsoft.com/wsdl/types/"]');
  InvRegistry.RegisterParamInfo(TypeInfo(IntegrationServiceSoap), 'GetPersonIdentifiers', 'GetPersonIdentifiersResult', '',
                                '[ArrayItemName="Identifier"]');
  { IntegrationServiceSoap.CreatePerson }
  InvRegistry.RegisterMethodInfo(TypeInfo(IntegrationServiceSoap), 'CreatePerson', '',
                                 '[ReturnName="CreatePersonResult"]', IS_OPTN);
  InvRegistry.RegisterParamInfo(TypeInfo(IntegrationServiceSoap), 'CreatePerson', 'sessionID', '',
                                '[Namespace="http://microsoft.com/wsdl/types/"]');
  { IntegrationServiceSoap.CreateVisitor }
  InvRegistry.RegisterMethodInfo(TypeInfo(IntegrationServiceSoap), 'CreateVisitor', '',
                                 '[ReturnName="CreateVisitorResult"]', IS_OPTN);
  InvRegistry.RegisterParamInfo(TypeInfo(IntegrationServiceSoap), 'CreateVisitor', 'sessionID', '',
                                '[Namespace="http://microsoft.com/wsdl/types/"]');
  { IntegrationServiceSoap.OpenPersonEditingSession }
  InvRegistry.RegisterMethodInfo(TypeInfo(IntegrationServiceSoap), 'OpenPersonEditingSession', '',
                                 '[ReturnName="OpenPersonEditingSessionResult"]', IS_OPTN);
  InvRegistry.RegisterParamInfo(TypeInfo(IntegrationServiceSoap), 'OpenPersonEditingSession', 'sessionID', '',
                                '[Namespace="http://microsoft.com/wsdl/types/"]');
  InvRegistry.RegisterParamInfo(TypeInfo(IntegrationServiceSoap), 'OpenPersonEditingSession', 'personID', '',
                                '[Namespace="http://microsoft.com/wsdl/types/"]');
  { IntegrationServiceSoap.ClosePersonEditingSession }
  InvRegistry.RegisterParamInfo(TypeInfo(IntegrationServiceSoap), 'ClosePersonEditingSession', 'personEditSessionID', '',
                                '[Namespace="http://microsoft.com/wsdl/types/"]');
  { IntegrationServiceSoap.SavePerson }
  InvRegistry.RegisterMethodInfo(TypeInfo(IntegrationServiceSoap), 'SavePerson', '',
                                 '[ReturnName="SavePersonResult"]', IS_OPTN);
  InvRegistry.RegisterParamInfo(TypeInfo(IntegrationServiceSoap), 'SavePerson', 'personEditSessionID', '',
                                '[Namespace="http://microsoft.com/wsdl/types/"]');
  { IntegrationServiceSoap.SetPersonPhoto }
  InvRegistry.RegisterMethodInfo(TypeInfo(IntegrationServiceSoap), 'SetPersonPhoto', '',
                                 '[ReturnName="SetPersonPhotoResult"]', IS_OPTN);
  InvRegistry.RegisterParamInfo(TypeInfo(IntegrationServiceSoap), 'SetPersonPhoto', 'personEditSessionID', '',
                                '[Namespace="http://microsoft.com/wsdl/types/"]');
  { IntegrationServiceSoap.SetPersonOrgUnit }
  InvRegistry.RegisterMethodInfo(TypeInfo(IntegrationServiceSoap), 'SetPersonOrgUnit', '',
                                 '[ReturnName="SetPersonOrgUnitResult"]', IS_OPTN);
  InvRegistry.RegisterParamInfo(TypeInfo(IntegrationServiceSoap), 'SetPersonOrgUnit', 'personEditSessionID', '',
                                '[Namespace="http://microsoft.com/wsdl/types/"]');
  InvRegistry.RegisterParamInfo(TypeInfo(IntegrationServiceSoap), 'SetPersonOrgUnit', 'orgUnitID', '',
                                '[Namespace="http://microsoft.com/wsdl/types/"]');
  { IntegrationServiceSoap.DeletePerson }
  InvRegistry.RegisterMethodInfo(TypeInfo(IntegrationServiceSoap), 'DeletePerson', '',
                                 '[ReturnName="DeletePersonResult"]', IS_OPTN);
  InvRegistry.RegisterParamInfo(TypeInfo(IntegrationServiceSoap), 'DeletePerson', 'sessionID', '',
                                '[Namespace="http://microsoft.com/wsdl/types/"]');
  InvRegistry.RegisterParamInfo(TypeInfo(IntegrationServiceSoap), 'DeletePerson', 'personID', '',
                                '[Namespace="http://microsoft.com/wsdl/types/"]');
  { IntegrationServiceSoap.CreateOrgUnit }
  InvRegistry.RegisterMethodInfo(TypeInfo(IntegrationServiceSoap), 'CreateOrgUnit', '',
                                 '[ReturnName="CreateOrgUnitResult"]', IS_OPTN);
  InvRegistry.RegisterParamInfo(TypeInfo(IntegrationServiceSoap), 'CreateOrgUnit', 'sessionID', '',
                                '[Namespace="http://microsoft.com/wsdl/types/"]');
  { IntegrationServiceSoap.OpenOrgUnitEditingSession }
  InvRegistry.RegisterMethodInfo(TypeInfo(IntegrationServiceSoap), 'OpenOrgUnitEditingSession', '',
                                 '[ReturnName="OpenOrgUnitEditingSessionResult"]', IS_OPTN);
  InvRegistry.RegisterParamInfo(TypeInfo(IntegrationServiceSoap), 'OpenOrgUnitEditingSession', 'sessionID', '',
                                '[Namespace="http://microsoft.com/wsdl/types/"]');
  InvRegistry.RegisterParamInfo(TypeInfo(IntegrationServiceSoap), 'OpenOrgUnitEditingSession', 'orgUnitID', '',
                                '[Namespace="http://microsoft.com/wsdl/types/"]');
  { IntegrationServiceSoap.CloseOrgUnitEditingSession }
  InvRegistry.RegisterParamInfo(TypeInfo(IntegrationServiceSoap), 'CloseOrgUnitEditingSession', 'orgUnitEditSessionID', '',
                                '[Namespace="http://microsoft.com/wsdl/types/"]');
  { IntegrationServiceSoap.SaveOrgUnit }
  InvRegistry.RegisterMethodInfo(TypeInfo(IntegrationServiceSoap), 'SaveOrgUnit', '',
                                 '[ReturnName="SaveOrgUnitResult"]', IS_OPTN);
  InvRegistry.RegisterParamInfo(TypeInfo(IntegrationServiceSoap), 'SaveOrgUnit', 'orgUnitEditSessionID', '',
                                '[Namespace="http://microsoft.com/wsdl/types/"]');
  { IntegrationServiceSoap.DeleteOrgUnit }
  InvRegistry.RegisterMethodInfo(TypeInfo(IntegrationServiceSoap), 'DeleteOrgUnit', '',
                                 '[ReturnName="DeleteOrgUnitResult"]', IS_OPTN);
  InvRegistry.RegisterParamInfo(TypeInfo(IntegrationServiceSoap), 'DeleteOrgUnit', 'sessionID', '',
                                '[Namespace="http://microsoft.com/wsdl/types/"]');
  InvRegistry.RegisterParamInfo(TypeInfo(IntegrationServiceSoap), 'DeleteOrgUnit', 'orgUnitID', '',
                                '[Namespace="http://microsoft.com/wsdl/types/"]');
  { IntegrationServiceSoap.DeleteIdentifier }
  InvRegistry.RegisterMethodInfo(TypeInfo(IntegrationServiceSoap), 'DeleteIdentifier', '',
                                 '[ReturnName="DeleteIdentifierResult"]', IS_OPTN);
  InvRegistry.RegisterParamInfo(TypeInfo(IntegrationServiceSoap), 'DeleteIdentifier', 'sessionID', '',
                                '[Namespace="http://microsoft.com/wsdl/types/"]');
  { IntegrationServiceSoap.AddPersonIdentifier }
  InvRegistry.RegisterMethodInfo(TypeInfo(IntegrationServiceSoap), 'AddPersonIdentifier', '',
                                 '[ReturnName="AddPersonIdentifierResult"]', IS_OPTN);
  InvRegistry.RegisterParamInfo(TypeInfo(IntegrationServiceSoap), 'AddPersonIdentifier', 'personEditSessionID', '',
                                '[Namespace="http://microsoft.com/wsdl/types/"]');
  { IntegrationServiceSoap.ChangePersonIdentifier }
  InvRegistry.RegisterMethodInfo(TypeInfo(IntegrationServiceSoap), 'ChangePersonIdentifier', '',
                                 '[ReturnName="ChangePersonIdentifierResult"]', IS_OPTN);
  InvRegistry.RegisterParamInfo(TypeInfo(IntegrationServiceSoap), 'ChangePersonIdentifier', 'personEditSessionID', '',
                                '[Namespace="http://microsoft.com/wsdl/types/"]');
  { IntegrationServiceSoap.GetTerritoriesHierarhy }
  InvRegistry.RegisterMethodInfo(TypeInfo(IntegrationServiceSoap), 'GetTerritoriesHierarhy', '',
                                 '[ReturnName="GetTerritoriesHierarhyResult"]', IS_OPTN);
  InvRegistry.RegisterParamInfo(TypeInfo(IntegrationServiceSoap), 'GetTerritoriesHierarhy', 'sessionID', '',
                                '[Namespace="http://microsoft.com/wsdl/types/"]');
  InvRegistry.RegisterParamInfo(TypeInfo(IntegrationServiceSoap), 'GetTerritoriesHierarhy', 'GetTerritoriesHierarhyResult', '',
                                '[ArrayItemName="Territory"]');
  { IntegrationServiceSoap.GetTerritorySubItems }
  InvRegistry.RegisterMethodInfo(TypeInfo(IntegrationServiceSoap), 'GetTerritorySubItems', '',
                                 '[ReturnName="GetTerritorySubItemsResult"]', IS_OPTN);
  InvRegistry.RegisterParamInfo(TypeInfo(IntegrationServiceSoap), 'GetTerritorySubItems', 'sessionID', '',
                                '[Namespace="http://microsoft.com/wsdl/types/"]');
  InvRegistry.RegisterParamInfo(TypeInfo(IntegrationServiceSoap), 'GetTerritorySubItems', 'TerraID', '',
                                '[Namespace="http://microsoft.com/wsdl/types/"]');
  InvRegistry.RegisterParamInfo(TypeInfo(IntegrationServiceSoap), 'GetTerritorySubItems', 'GetTerritorySubItemsResult', '',
                                '[ArrayItemName="BaseTerritory"]');
  { IntegrationServiceSoap.GetEvents }
  InvRegistry.RegisterMethodInfo(TypeInfo(IntegrationServiceSoap), 'GetEvents', '',
                                 '[ReturnName="GetEventsResult"]', IS_OPTN);
  InvRegistry.RegisterParamInfo(TypeInfo(IntegrationServiceSoap), 'GetEvents', 'sessionID', '',
                                '[Namespace="http://microsoft.com/wsdl/types/"]');
  InvRegistry.RegisterParamInfo(TypeInfo(IntegrationServiceSoap), 'GetEvents', 'TerritoryID', '',
                                '[Namespace="http://microsoft.com/wsdl/types/"]');
  InvRegistry.RegisterParamInfo(TypeInfo(IntegrationServiceSoap), 'GetEvents', 'PersNodeID', '',
                                '[Namespace="http://microsoft.com/wsdl/types/"]');
  { IntegrationServiceSoap.GetPersonExtraFieldTemplates }
  InvRegistry.RegisterMethodInfo(TypeInfo(IntegrationServiceSoap), 'GetPersonExtraFieldTemplates', '',
                                 '[ReturnName="GetPersonExtraFieldTemplatesResult"]', IS_OPTN);
  InvRegistry.RegisterParamInfo(TypeInfo(IntegrationServiceSoap), 'GetPersonExtraFieldTemplates', 'sessionID', '',
                                '[Namespace="http://microsoft.com/wsdl/types/"]');
  InvRegistry.RegisterParamInfo(TypeInfo(IntegrationServiceSoap), 'GetPersonExtraFieldTemplates', 'GetPersonExtraFieldTemplatesResult', '',
                                '[ArrayItemName="PersonExtraFieldTemplate"]');
  { IntegrationServiceSoap.GetPersonExtraFieldValue }
  InvRegistry.RegisterMethodInfo(TypeInfo(IntegrationServiceSoap), 'GetPersonExtraFieldValue', '',
                                 '[ReturnName="GetPersonExtraFieldValueResult"]', IS_OPTN);
  InvRegistry.RegisterParamInfo(TypeInfo(IntegrationServiceSoap), 'GetPersonExtraFieldValue', 'sessionID', '',
                                '[Namespace="http://microsoft.com/wsdl/types/"]');
  InvRegistry.RegisterParamInfo(TypeInfo(IntegrationServiceSoap), 'GetPersonExtraFieldValue', 'personID', '',
                                '[Namespace="http://microsoft.com/wsdl/types/"]');
  InvRegistry.RegisterParamInfo(TypeInfo(IntegrationServiceSoap), 'GetPersonExtraFieldValue', 'templateID', '',
                                '[Namespace="http://microsoft.com/wsdl/types/"]');
  { IntegrationServiceSoap.GetPersonExtraFieldValueString }
  InvRegistry.RegisterMethodInfo(TypeInfo(IntegrationServiceSoap), 'GetPersonExtraFieldValueString', '',
                                 '[ReturnName="GetPersonExtraFieldValueStringResult"]', IS_OPTN);
  InvRegistry.RegisterParamInfo(TypeInfo(IntegrationServiceSoap), 'GetPersonExtraFieldValueString', 'sessionID', '',
                                '[Namespace="http://microsoft.com/wsdl/types/"]');
  InvRegistry.RegisterParamInfo(TypeInfo(IntegrationServiceSoap), 'GetPersonExtraFieldValueString', 'personID', '',
                                '[Namespace="http://microsoft.com/wsdl/types/"]');
  InvRegistry.RegisterParamInfo(TypeInfo(IntegrationServiceSoap), 'GetPersonExtraFieldValueString', 'templateID', '',
                                '[Namespace="http://microsoft.com/wsdl/types/"]');
  { IntegrationServiceSoap.SetPersonExtraFieldValue }
  InvRegistry.RegisterMethodInfo(TypeInfo(IntegrationServiceSoap), 'SetPersonExtraFieldValue', '',
                                 '[ReturnName="SetPersonExtraFieldValueResult"]', IS_OPTN);
  InvRegistry.RegisterParamInfo(TypeInfo(IntegrationServiceSoap), 'SetPersonExtraFieldValue', 'personEditSessionID', '',
                                '[Namespace="http://microsoft.com/wsdl/types/"]');
  InvRegistry.RegisterParamInfo(TypeInfo(IntegrationServiceSoap), 'SetPersonExtraFieldValue', 'templateID', '',
                                '[Namespace="http://microsoft.com/wsdl/types/"]');
  { IntegrationServiceSoap.GetPersonExtraFieldValues }
  InvRegistry.RegisterMethodInfo(TypeInfo(IntegrationServiceSoap), 'GetPersonExtraFieldValues', '',
                                 '[ReturnName="GetPersonExtraFieldValuesResult"]', IS_OPTN);
  InvRegistry.RegisterParamInfo(TypeInfo(IntegrationServiceSoap), 'GetPersonExtraFieldValues', 'sessionID', '',
                                '[Namespace="http://microsoft.com/wsdl/types/"]');
  InvRegistry.RegisterParamInfo(TypeInfo(IntegrationServiceSoap), 'GetPersonExtraFieldValues', 'personID', '',
                                '[Namespace="http://microsoft.com/wsdl/types/"]');
  InvRegistry.RegisterParamInfo(TypeInfo(IntegrationServiceSoap), 'GetPersonExtraFieldValues', 'GetPersonExtraFieldValuesResult', '',
                                '[ArrayItemName="ExtraFieldValue"]');
  { IntegrationServiceSoap.SetPersonExtraFieldValues }
  InvRegistry.RegisterMethodInfo(TypeInfo(IntegrationServiceSoap), 'SetPersonExtraFieldValues', '',
                                 '[ReturnName="SetPersonExtraFieldValuesResult"]', IS_OPTN);
  InvRegistry.RegisterParamInfo(TypeInfo(IntegrationServiceSoap), 'SetPersonExtraFieldValues', 'personEditSessionID', '',
                                '[Namespace="http://microsoft.com/wsdl/types/"]');
  InvRegistry.RegisterParamInfo(TypeInfo(IntegrationServiceSoap), 'SetPersonExtraFieldValues', 'values', '',
                                '[ArrayItemName="ExtraFieldValue"]');
  { IntegrationServiceSoap.ValidateExtraFieldValue }
  InvRegistry.RegisterMethodInfo(TypeInfo(IntegrationServiceSoap), 'ValidateExtraFieldValue', '',
                                 '[ReturnName="ValidateExtraFieldValueResult"]', IS_OPTN);
  InvRegistry.RegisterParamInfo(TypeInfo(IntegrationServiceSoap), 'ValidateExtraFieldValue', 'sessionID', '',
                                '[Namespace="http://microsoft.com/wsdl/types/"]');
  { IntegrationServiceSoap.FindVisitorRequest }
  InvRegistry.RegisterMethodInfo(TypeInfo(IntegrationServiceSoap), 'FindVisitorRequest', '',
                                 '[ReturnName="FindVisitorRequestResult"]', IS_OPTN);
  InvRegistry.RegisterParamInfo(TypeInfo(IntegrationServiceSoap), 'FindVisitorRequest', 'sessionID', '',
                                '[Namespace="http://microsoft.com/wsdl/types/"]');
  { IntegrationServiceSoap.GetAcceptedVisitorRequests }
  InvRegistry.RegisterMethodInfo(TypeInfo(IntegrationServiceSoap), 'GetAcceptedVisitorRequests', '',
                                 '[ReturnName="GetAcceptedVisitorRequestsResult"]', IS_OPTN);
  InvRegistry.RegisterParamInfo(TypeInfo(IntegrationServiceSoap), 'GetAcceptedVisitorRequests', 'sessionID', '',
                                '[Namespace="http://microsoft.com/wsdl/types/"]');
  InvRegistry.RegisterParamInfo(TypeInfo(IntegrationServiceSoap), 'GetAcceptedVisitorRequests', 'GetAcceptedVisitorRequestsResult', '',
                                '[ArrayItemName="VisitorRequest"]');
  { IntegrationServiceSoap.GetPersonVisitorRequests }
  InvRegistry.RegisterMethodInfo(TypeInfo(IntegrationServiceSoap), 'GetPersonVisitorRequests', '',
                                 '[ReturnName="GetPersonVisitorRequestsResult"]', IS_OPTN);
  InvRegistry.RegisterParamInfo(TypeInfo(IntegrationServiceSoap), 'GetPersonVisitorRequests', 'sessionID', '',
                                '[Namespace="http://microsoft.com/wsdl/types/"]');
  InvRegistry.RegisterParamInfo(TypeInfo(IntegrationServiceSoap), 'GetPersonVisitorRequests', 'visitorID', '',
                                '[Namespace="http://microsoft.com/wsdl/types/"]');
  InvRegistry.RegisterParamInfo(TypeInfo(IntegrationServiceSoap), 'GetPersonVisitorRequests', 'GetPersonVisitorRequestsResult', '',
                                '[ArrayItemName="VisitorRequest"]');
  { IntegrationServiceSoap.ActivateVisitorRequest }
  InvRegistry.RegisterMethodInfo(TypeInfo(IntegrationServiceSoap), 'ActivateVisitorRequest', '',
                                 '[ReturnName="ActivateVisitorRequestResult"]', IS_OPTN);
  InvRegistry.RegisterParamInfo(TypeInfo(IntegrationServiceSoap), 'ActivateVisitorRequest', 'sessionID', '',
                                '[Namespace="http://microsoft.com/wsdl/types/"]');
  InvRegistry.RegisterParamInfo(TypeInfo(IntegrationServiceSoap), 'ActivateVisitorRequest', 'requestID', '',
                                '[Namespace="http://microsoft.com/wsdl/types/"]');
  { IntegrationServiceSoap.CloseVisitorRequest }
  InvRegistry.RegisterMethodInfo(TypeInfo(IntegrationServiceSoap), 'CloseVisitorRequest', '',
                                 '[ReturnName="CloseVisitorRequestResult"]', IS_OPTN);
  InvRegistry.RegisterParamInfo(TypeInfo(IntegrationServiceSoap), 'CloseVisitorRequest', 'sessionID', '',
                                '[Namespace="http://microsoft.com/wsdl/types/"]');
  InvRegistry.RegisterParamInfo(TypeInfo(IntegrationServiceSoap), 'CloseVisitorRequest', 'requestID', '',
                                '[Namespace="http://microsoft.com/wsdl/types/"]');
  { IntegrationServiceSoap.CloseAllActiveVisitorRequests }
  InvRegistry.RegisterMethodInfo(TypeInfo(IntegrationServiceSoap), 'CloseAllActiveVisitorRequests', '',
                                 '[ReturnName="CloseAllActiveVisitorRequestsResult"]', IS_OPTN);
  InvRegistry.RegisterParamInfo(TypeInfo(IntegrationServiceSoap), 'CloseAllActiveVisitorRequests', 'sessionID', '',
                                '[Namespace="http://microsoft.com/wsdl/types/"]');
  InvRegistry.RegisterParamInfo(TypeInfo(IntegrationServiceSoap), 'CloseAllActiveVisitorRequests', 'visitorID', '',
                                '[Namespace="http://microsoft.com/wsdl/types/"]');
  { IntegrationServiceSoap.FindPeople }
  InvRegistry.RegisterMethodInfo(TypeInfo(IntegrationServiceSoap), 'FindPeople', '',
                                 '[ReturnName="FindPeopleResult"]', IS_OPTN);
  InvRegistry.RegisterParamInfo(TypeInfo(IntegrationServiceSoap), 'FindPeople', 'sessionID', '',
                                '[Namespace="http://microsoft.com/wsdl/types/"]');
  InvRegistry.RegisterParamInfo(TypeInfo(IntegrationServiceSoap), 'FindPeople', 'FindPeopleResult', '',
                                '[ArrayItemName="Person"]');
  { IntegrationServiceSoap.FindVisitors }
  InvRegistry.RegisterMethodInfo(TypeInfo(IntegrationServiceSoap), 'FindVisitors', '',
                                 '[ReturnName="FindVisitorsResult"]', IS_OPTN);
  InvRegistry.RegisterParamInfo(TypeInfo(IntegrationServiceSoap), 'FindVisitors', 'sessionID', '',
                                '[Namespace="http://microsoft.com/wsdl/types/"]');
  InvRegistry.RegisterParamInfo(TypeInfo(IntegrationServiceSoap), 'FindVisitors', 'FindVisitorsResult', '',
                                '[ArrayItemName="Person"]');
  { IntegrationServiceSoap.GetAccessSchedules }
  InvRegistry.RegisterMethodInfo(TypeInfo(IntegrationServiceSoap), 'GetAccessSchedules', '',
                                 '[ReturnName="GetAccessSchedulesResult"]', IS_OPTN);
  InvRegistry.RegisterParamInfo(TypeInfo(IntegrationServiceSoap), 'GetAccessSchedules', 'sessionID', '',
                                '[Namespace="http://microsoft.com/wsdl/types/"]');
  InvRegistry.RegisterParamInfo(TypeInfo(IntegrationServiceSoap), 'GetAccessSchedules', 'GetAccessSchedulesResult', '',
                                '[ArrayItemName="Schedule"]');
  { IntegrationServiceSoap.GetScheduleIntervals }
  InvRegistry.RegisterMethodInfo(TypeInfo(IntegrationServiceSoap), 'GetScheduleIntervals', '',
                                 '[ReturnName="GetScheduleIntervalsResult"]', IS_OPTN);
  InvRegistry.RegisterParamInfo(TypeInfo(IntegrationServiceSoap), 'GetScheduleIntervals', 'sessionID', '',
                                '[Namespace="http://microsoft.com/wsdl/types/"]');
  InvRegistry.RegisterParamInfo(TypeInfo(IntegrationServiceSoap), 'GetScheduleIntervals', 'scheduleID', '',
                                '[Namespace="http://microsoft.com/wsdl/types/"]');
  InvRegistry.RegisterParamInfo(TypeInfo(IntegrationServiceSoap), 'GetScheduleIntervals', 'to_', 'to', '');
  InvRegistry.RegisterParamInfo(TypeInfo(IntegrationServiceSoap), 'GetScheduleIntervals', 'GetScheduleIntervalsResult', '',
                                '[ArrayItemName="TimeInterval"]');
  { IntegrationServiceSoap.CreateTempAccessGroup }
  InvRegistry.RegisterMethodInfo(TypeInfo(IntegrationServiceSoap), 'CreateTempAccessGroup', '',
                                 '[ReturnName="CreateTempAccessGroupResult"]', IS_OPTN);
  InvRegistry.RegisterParamInfo(TypeInfo(IntegrationServiceSoap), 'CreateTempAccessGroup', 'sessionID', '',
                                '[Namespace="http://microsoft.com/wsdl/types/"]');
  InvRegistry.RegisterParamInfo(TypeInfo(IntegrationServiceSoap), 'CreateTempAccessGroup', 'scheduleID', '',
                                '[Namespace="http://microsoft.com/wsdl/types/"]');
  InvRegistry.RegisterParamInfo(TypeInfo(IntegrationServiceSoap), 'CreateTempAccessGroup', 'territories', '',
                                '[ArrayItemName="guid"]');
  { IntegrationServiceSoap.GetRootTerritory }
  InvRegistry.RegisterMethodInfo(TypeInfo(IntegrationServiceSoap), 'GetRootTerritory', '',
                                 '[ReturnName="GetRootTerritoryResult"]', IS_OPTN);
  InvRegistry.RegisterParamInfo(TypeInfo(IntegrationServiceSoap), 'GetRootTerritory', 'sessionID', '',
                                '[Namespace="http://microsoft.com/wsdl/types/"]');
  { IntegrationServiceSoap.GetTerritory }
  InvRegistry.RegisterMethodInfo(TypeInfo(IntegrationServiceSoap), 'GetTerritory', '',
                                 '[ReturnName="GetTerritoryResult"]', IS_OPTN);
  InvRegistry.RegisterParamInfo(TypeInfo(IntegrationServiceSoap), 'GetTerritory', 'sessionID', '',
                                '[Namespace="http://microsoft.com/wsdl/types/"]');
  InvRegistry.RegisterParamInfo(TypeInfo(IntegrationServiceSoap), 'GetTerritory', 'territoryID', '',
                                '[Namespace="http://microsoft.com/wsdl/types/"]');
  { IntegrationServiceSoap.GetRootOrgUnit }
  InvRegistry.RegisterMethodInfo(TypeInfo(IntegrationServiceSoap), 'GetRootOrgUnit', '',
                                 '[ReturnName="GetRootOrgUnitResult"]', IS_OPTN);
  InvRegistry.RegisterParamInfo(TypeInfo(IntegrationServiceSoap), 'GetRootOrgUnit', 'sessionID', '',
                                '[Namespace="http://microsoft.com/wsdl/types/"]');
  { IntegrationServiceSoap.GetDomains }
  InvRegistry.RegisterMethodInfo(TypeInfo(IntegrationServiceSoap), 'GetDomains', '',
                                 '[ReturnName="GetDomainsResult"]', IS_OPTN);
  InvRegistry.RegisterParamInfo(TypeInfo(IntegrationServiceSoap), 'GetDomains', 'GetDomainsResult', '',
                                '[ArrayItemName="Domain"]');
  { IntegrationServiceSoap.CreateVisitorRequest }
  InvRegistry.RegisterMethodInfo(TypeInfo(IntegrationServiceSoap), 'CreateVisitorRequest', '',
                                 '[ReturnName="CreateVisitorRequestResult"]', IS_OPTN);
  InvRegistry.RegisterParamInfo(TypeInfo(IntegrationServiceSoap), 'CreateVisitorRequest', 'sessionID', '',
                                '[Namespace="http://microsoft.com/wsdl/types/"]');
  { IntegrationServiceSoap.DeleteIssuedVisitorRequest }
  InvRegistry.RegisterMethodInfo(TypeInfo(IntegrationServiceSoap), 'DeleteIssuedVisitorRequest', '',
                                 '[ReturnName="DeleteIssuedVisitorRequestResult"]', IS_OPTN);
  InvRegistry.RegisterParamInfo(TypeInfo(IntegrationServiceSoap), 'DeleteIssuedVisitorRequest', 'sessionID', '',
                                '[Namespace="http://microsoft.com/wsdl/types/"]');
  InvRegistry.RegisterParamInfo(TypeInfo(IntegrationServiceSoap), 'DeleteIssuedVisitorRequest', 'requestID', '',
                                '[Namespace="http://microsoft.com/wsdl/types/"]');
  { IntegrationServiceSoap.GetIssuedVisitorRequests }
  InvRegistry.RegisterMethodInfo(TypeInfo(IntegrationServiceSoap), 'GetIssuedVisitorRequests', '',
                                 '[ReturnName="GetIssuedVisitorRequestsResult"]', IS_OPTN);
  InvRegistry.RegisterParamInfo(TypeInfo(IntegrationServiceSoap), 'GetIssuedVisitorRequests', 'sessionID', '',
                                '[Namespace="http://microsoft.com/wsdl/types/"]');
  InvRegistry.RegisterParamInfo(TypeInfo(IntegrationServiceSoap), 'GetIssuedVisitorRequests', 'GetIssuedVisitorRequestsResult', '',
                                '[ArrayItemName="VisitorRequest"]');
  { IntegrationServiceSoap.GetVisitorRequest }
  InvRegistry.RegisterMethodInfo(TypeInfo(IntegrationServiceSoap), 'GetVisitorRequest', '',
                                 '[ReturnName="GetVisitorRequestResult"]', IS_OPTN);
  InvRegistry.RegisterParamInfo(TypeInfo(IntegrationServiceSoap), 'GetVisitorRequest', 'sessionID', '',
                                '[Namespace="http://microsoft.com/wsdl/types/"]');
  InvRegistry.RegisterParamInfo(TypeInfo(IntegrationServiceSoap), 'GetVisitorRequest', 'requestID', '',
                                '[Namespace="http://microsoft.com/wsdl/types/"]');
  { IntegrationServiceSoap.SaveVisitorRequest }
  InvRegistry.RegisterMethodInfo(TypeInfo(IntegrationServiceSoap), 'SaveVisitorRequest', '',
                                 '[ReturnName="SaveVisitorRequestResult"]', IS_OPTN);
  InvRegistry.RegisterParamInfo(TypeInfo(IntegrationServiceSoap), 'SaveVisitorRequest', 'sessionID', '',
                                '[Namespace="http://microsoft.com/wsdl/types/"]');
  { IntegrationServiceSoap.OpenEventHistorySession }
  InvRegistry.RegisterMethodInfo(TypeInfo(IntegrationServiceSoap), 'OpenEventHistorySession', '',
                                 '[ReturnName="OpenEventHistorySessionResult"]', IS_OPTN);
  InvRegistry.RegisterParamInfo(TypeInfo(IntegrationServiceSoap), 'OpenEventHistorySession', 'sessionID', '',
                                '[Namespace="http://microsoft.com/wsdl/types/"]');
  { IntegrationServiceSoap.CloseEventHistorySession }
  InvRegistry.RegisterParamInfo(TypeInfo(IntegrationServiceSoap), 'CloseEventHistorySession', 'sessionID', '',
                                '[Namespace="http://microsoft.com/wsdl/types/"]');
  InvRegistry.RegisterParamInfo(TypeInfo(IntegrationServiceSoap), 'CloseEventHistorySession', 'eventHistorySessionID', '',
                                '[Namespace="http://microsoft.com/wsdl/types/"]');
  { IntegrationServiceSoap.GetEventHistoryResultCount }
  InvRegistry.RegisterMethodInfo(TypeInfo(IntegrationServiceSoap), 'GetEventHistoryResultCount', '',
                                 '[ReturnName="GetEventHistoryResultCountResult"]');
  InvRegistry.RegisterParamInfo(TypeInfo(IntegrationServiceSoap), 'GetEventHistoryResultCount', 'sessionID', '',
                                '[Namespace="http://microsoft.com/wsdl/types/"]');
  InvRegistry.RegisterParamInfo(TypeInfo(IntegrationServiceSoap), 'GetEventHistoryResultCount', 'eventHistorySessionID', '',
                                '[Namespace="http://microsoft.com/wsdl/types/"]');
  { IntegrationServiceSoap.GetEventHistoryResult }
  InvRegistry.RegisterMethodInfo(TypeInfo(IntegrationServiceSoap), 'GetEventHistoryResult', '',
                                 '[ReturnName="GetEventHistoryResultResult"]', IS_OPTN);
  InvRegistry.RegisterParamInfo(TypeInfo(IntegrationServiceSoap), 'GetEventHistoryResult', 'sessionID', '',
                                '[Namespace="http://microsoft.com/wsdl/types/"]');
  InvRegistry.RegisterParamInfo(TypeInfo(IntegrationServiceSoap), 'GetEventHistoryResult', 'eventHistorySessionID', '',
                                '[Namespace="http://microsoft.com/wsdl/types/"]');
  InvRegistry.RegisterParamInfo(TypeInfo(IntegrationServiceSoap), 'GetEventHistoryResult', 'fields', '',
                                '[ArrayItemName="guid"]');
  InvRegistry.RegisterParamInfo(TypeInfo(IntegrationServiceSoap), 'GetEventHistoryResult', 'GetEventHistoryResultResult', '',
                                '[ArrayItemName="EventObject"]');
  { IntegrationServiceSoap.FindPersonByIdentifier }
  InvRegistry.RegisterMethodInfo(TypeInfo(IntegrationServiceSoap), 'FindPersonByIdentifier', '',
                                 '[ReturnName="FindPersonByIdentifierResult"]', IS_OPTN);
  InvRegistry.RegisterParamInfo(TypeInfo(IntegrationServiceSoap), 'FindPersonByIdentifier', 'sessionID', '',
                                '[Namespace="http://microsoft.com/wsdl/types/"]');
  { IntegrationServiceSoap.BlockPerson }
  InvRegistry.RegisterMethodInfo(TypeInfo(IntegrationServiceSoap), 'BlockPerson', '',
                                 '[ReturnName="BlockPersonResult"]', IS_OPTN);
  InvRegistry.RegisterParamInfo(TypeInfo(IntegrationServiceSoap), 'BlockPerson', 'personEditSessionID', '',
                                '[Namespace="http://microsoft.com/wsdl/types/"]');
  { IntegrationServiceSoap.UnblockPerson }
  InvRegistry.RegisterMethodInfo(TypeInfo(IntegrationServiceSoap), 'UnblockPerson', '',
                                 '[ReturnName="UnblockPersonResult"]', IS_OPTN);
  InvRegistry.RegisterParamInfo(TypeInfo(IntegrationServiceSoap), 'UnblockPerson', 'personEditSessionID', '',
                                '[Namespace="http://microsoft.com/wsdl/types/"]');
  RemClassRegistry.RegisterXSInfo(TypeInfo(ArrayOfOrgUnit), 'http://parsec.ru/Parsec3IntergationService', 'ArrayOfOrgUnit');
  RemClassRegistry.RegisterXSInfo(TypeInfo(ArrayOfBaseObject), 'http://parsec.ru/Parsec3IntergationService', 'ArrayOfBaseObject');
  RemClassRegistry.RegisterXSInfo(TypeInfo(ArrayOfTerritory), 'http://parsec.ru/Parsec3IntergationService', 'ArrayOfTerritory');
  RemClassRegistry.RegisterXSInfo(TypeInfo(ArrayOfAccessGroup), 'http://parsec.ru/Parsec3IntergationService', 'ArrayOfAccessGroup');
  RemClassRegistry.RegisterXSInfo(TypeInfo(ArrayOfIdentifier), 'http://parsec.ru/Parsec3IntergationService', 'ArrayOfIdentifier');
  RemClassRegistry.RegisterXSInfo(TypeInfo(guid), 'http://microsoft.com/wsdl/types/', 'guid');
  RemClassRegistry.RegisterXSClass(AccessGroup, 'http://parsec.ru/Parsec3IntergationService', 'AccessGroup');
  RemClassRegistry.RegisterExternalPropName(TypeInfo(AccessGroup), 'NAME_', '[ExtName="NAME"]');
  RemClassRegistry.RegisterXSClass(BaseResult, 'http://parsec.ru/Parsec3IntergationService', 'BaseResult');
  RemClassRegistry.RegisterXSClass(SessionResult, 'http://parsec.ru/Parsec3IntergationService', 'SessionResult');
  RemClassRegistry.RegisterXSClass(GuidResult, 'http://parsec.ru/Parsec3IntergationService', 'GuidResult');
  RemClassRegistry.RegisterXSClass(Session, 'http://parsec.ru/Parsec3IntergationService', 'Session');
  RemClassRegistry.RegisterXSClass(BaseObject, 'http://parsec.ru/Parsec3IntergationService', 'BaseObject');
  RemClassRegistry.RegisterXSClass(BaseOrgUnit, 'http://parsec.ru/Parsec3IntergationService', 'BaseOrgUnit');
  RemClassRegistry.RegisterExternalPropName(TypeInfo(BaseOrgUnit), 'NAME_', '[ExtName="NAME"]');
  RemClassRegistry.RegisterXSClass(OrgUnit, 'http://parsec.ru/Parsec3IntergationService', 'OrgUnit');
  RemClassRegistry.RegisterXSClass(BaseIdentifier, 'http://parsec.ru/Parsec3IntergationService', 'BaseIdentifier');
  RemClassRegistry.RegisterXSClass(Identifier, 'http://parsec.ru/Parsec3IntergationService', 'Identifier');
  RemClassRegistry.RegisterXSClass(IdentifierTemp, 'http://parsec.ru/Parsec3IntergationService', 'IdentifierTemp');
  RemClassRegistry.RegisterXSClass(StockIdentifier, 'http://parsec.ru/Parsec3IntergationService', 'StockIdentifier');
  RemClassRegistry.RegisterXSClass(BasePerson, 'http://parsec.ru/Parsec3IntergationService', 'BasePerson');
  RemClassRegistry.RegisterXSClass(Person, 'http://parsec.ru/Parsec3IntergationService', 'Person');
  RemClassRegistry.RegisterXSClass(PersonWithPhoto, 'http://parsec.ru/Parsec3IntergationService', 'PersonWithPhoto');
  RemClassRegistry.RegisterXSClass(Event, 'http://parsec.ru/Parsec3IntergationService', 'Event');
  RemClassRegistry.RegisterXSInfo(TypeInfo(ArrayOfTimeInterval), 'http://parsec.ru/Parsec3IntergationService', 'ArrayOfTimeInterval');
  RemClassRegistry.RegisterXSClass(TimeInterval, 'http://parsec.ru/Parsec3IntergationService', 'TimeInterval');
  RemClassRegistry.RegisterExternalPropName(TypeInfo(TimeInterval), 'END_', '[ExtName="END"]');
  RemClassRegistry.RegisterXSInfo(TypeInfo(ArrayOfDomain), 'http://parsec.ru/Parsec3IntergationService', 'ArrayOfDomain');
  RemClassRegistry.RegisterXSClass(Schedule, 'http://parsec.ru/Parsec3IntergationService', 'Schedule');
  RemClassRegistry.RegisterExternalPropName(TypeInfo(Schedule), 'NAME_', '[ExtName="NAME"]');
  RemClassRegistry.RegisterXSInfo(TypeInfo(ArrayOfVisitorRequest), 'http://parsec.ru/Parsec3IntergationService', 'ArrayOfVisitorRequest');
  RemClassRegistry.RegisterXSInfo(TypeInfo(ArrayOfPerson), 'http://parsec.ru/Parsec3IntergationService', 'ArrayOfPerson');
  RemClassRegistry.RegisterXSInfo(TypeInfo(ArrayOfSchedule), 'http://parsec.ru/Parsec3IntergationService', 'ArrayOfSchedule');
  RemClassRegistry.RegisterXSInfo(TypeInfo(ArrayOfAnyType), 'http://parsec.ru/Parsec3IntergationService', 'ArrayOfAnyType');
  RemClassRegistry.RegisterXSInfo(TypeInfo(ArrayOfEventObject), 'http://parsec.ru/Parsec3IntergationService', 'ArrayOfEventObject');
  RemClassRegistry.RegisterXSClass(EventObject, 'http://parsec.ru/Parsec3IntergationService', 'EventObject');
  RemClassRegistry.RegisterExternalPropName(TypeInfo(EventObject), 'Values', '[ArrayItemName="anyType"]');
  RemClassRegistry.RegisterXSClass(Domain, 'http://parsec.ru/Parsec3IntergationService', 'Domain');
  RemClassRegistry.RegisterExternalPropName(TypeInfo(Domain), 'NAME_', '[ExtName="NAME"]');
  RemClassRegistry.RegisterXSInfo(TypeInfo(ArrayOfUnsignedInt), 'http://parsec.ru/Parsec3IntergationService', 'ArrayOfUnsignedInt');
  RemClassRegistry.RegisterXSClass(VisitorRequest, 'http://parsec.ru/Parsec3IntergationService', 'VisitorRequest');
  RemClassRegistry.RegisterXSInfo(TypeInfo(ArrayOfEvent), 'http://parsec.ru/Parsec3IntergationService', 'ArrayOfEvent');
  RemClassRegistry.RegisterXSInfo(TypeInfo(ArrayOfGuid), 'http://parsec.ru/Parsec3IntergationService', 'ArrayOfGuid');
  RemClassRegistry.RegisterXSClass(EventHistoryQueryParams, 'http://parsec.ru/Parsec3IntergationService', 'EventHistoryQueryParams');
  RemClassRegistry.RegisterExternalPropName(TypeInfo(EventHistoryQueryParams), 'IDs', '[ArrayItemName="guid"]');
  RemClassRegistry.RegisterExternalPropName(TypeInfo(EventHistoryQueryParams), 'Territories', '[ArrayItemName="guid"]');
  RemClassRegistry.RegisterExternalPropName(TypeInfo(EventHistoryQueryParams), 'Operators', '[ArrayItemName="guid"]');
  RemClassRegistry.RegisterExternalPropName(TypeInfo(EventHistoryQueryParams), 'TransactionTypes', '[ArrayItemName="unsignedInt"]');
  RemClassRegistry.RegisterExternalPropName(TypeInfo(EventHistoryQueryParams), 'Organizations', '[ArrayItemName="guid"]');
  RemClassRegistry.RegisterExternalPropName(TypeInfo(EventHistoryQueryParams), 'Users', '[ArrayItemName="guid"]');
  RemClassRegistry.RegisterXSInfo(TypeInfo(ArrayOfString), 'http://parsec.ru/Parsec3IntergationService', 'ArrayOfString');
  RemClassRegistry.RegisterXSClass(EventsHistory, 'http://parsec.ru/Parsec3IntergationService', 'EventsHistory');
  RemClassRegistry.RegisterExternalPropName(TypeInfo(EventsHistory), 'Events', '[ArrayItemName="Event"]');
  RemClassRegistry.RegisterExternalPropName(TypeInfo(EventsHistory), 'Persons', '[ArrayItemName="guid"]');
  RemClassRegistry.RegisterExternalPropName(TypeInfo(EventsHistory), 'PersonFullNames', '[ArrayItemName="string"]');
  RemClassRegistry.RegisterExternalPropName(TypeInfo(EventsHistory), 'Territories', '[ArrayItemName="guid"]');
  RemClassRegistry.RegisterExternalPropName(TypeInfo(EventsHistory), 'TerritoryNames', '[ArrayItemName="string"]');
  RemClassRegistry.RegisterXSClass(BaseTerritory, 'http://parsec.ru/Parsec3IntergationService', 'BaseTerritory');
  RemClassRegistry.RegisterExternalPropName(TypeInfo(BaseTerritory), 'TYPE_', '[ExtName="TYPE"]');
  RemClassRegistry.RegisterExternalPropName(TypeInfo(BaseTerritory), 'NAME_', '[ExtName="NAME"]');
  RemClassRegistry.RegisterXSClass(Territory, 'http://parsec.ru/Parsec3IntergationService', 'Territory');
  RemClassRegistry.RegisterXSInfo(TypeInfo(ArrayOfBaseTerritory), 'http://parsec.ru/Parsec3IntergationService', 'ArrayOfBaseTerritory');
  RemClassRegistry.RegisterXSClass(EventsHistoryResult, 'http://parsec.ru/Parsec3IntergationService', 'EventsHistoryResult');
  RemClassRegistry.RegisterXSClass(StringResult, 'http://parsec.ru/Parsec3IntergationService', 'StringResult');
  RemClassRegistry.RegisterXSInfo(TypeInfo(ArrayOfExtraFieldValue), 'http://parsec.ru/Parsec3IntergationService', 'ArrayOfExtraFieldValue');
  RemClassRegistry.RegisterXSClass(ExtraFieldValue, 'http://parsec.ru/Parsec3IntergationService', 'ExtraFieldValue');
  RemClassRegistry.RegisterXSClass(ObjectResult, 'http://parsec.ru/Parsec3IntergationService', 'ObjectResult');
  RemClassRegistry.RegisterXSInfo(TypeInfo(ArrayOfPersonExtraFieldTemplate), 'http://parsec.ru/Parsec3IntergationService', 'ArrayOfPersonExtraFieldTemplate');
  RemClassRegistry.RegisterXSInfo(TypeInfo(XmlTypeCode), 'http://parsec.ru/Parsec3IntergationService', 'XmlTypeCode');
  RemClassRegistry.RegisterExternalPropName(TypeInfo(XmlTypeCode), 'String_', 'String');
  RemClassRegistry.RegisterExternalPropName(TypeInfo(XmlTypeCode), 'Boolean_', 'Boolean');
  RemClassRegistry.RegisterExternalPropName(TypeInfo(XmlTypeCode), 'Double_', 'Double');
  RemClassRegistry.RegisterExternalPropName(TypeInfo(XmlTypeCode), 'Name_', 'Name');
  RemClassRegistry.RegisterExternalPropName(TypeInfo(XmlTypeCode), 'Integer_', 'Integer');
  RemClassRegistry.RegisterExternalPropName(TypeInfo(XmlTypeCode), 'Byte_', 'Byte');
  RemClassRegistry.RegisterXSClass(PersonExtraFieldTemplate, 'http://parsec.ru/Parsec3IntergationService', 'PersonExtraFieldTemplate');
  RemClassRegistry.RegisterExternalPropName(TypeInfo(PersonExtraFieldTemplate), 'TYPE_', '[ExtName="TYPE"]');
  RemClassRegistry.RegisterExternalPropName(TypeInfo(PersonExtraFieldTemplate), 'NAME_', '[ExtName="NAME"]');

end.
