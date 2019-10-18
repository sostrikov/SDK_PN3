using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using ParsecIntegrationClient.IntegrationWebService;

namespace ParsecIntegrationClient
{
	public partial class DeviceState : Form
	{
		public class StateDescription
		{
			public string Name;
			public string BitSet;
			public string BitClear;
			public int bitIndex;
		}

		public class CommandDescription
		{
			public string Name { get; set; }
			public int Code { get; set; }
		}

		private List<CommandDescription> _commands = null;
		private List<StateDescription> _states = null;

		private Guid _territoryID = Guid.Empty;
		private const string _noneTerritory = @"<не выбрано>";

		public DeviceState ()
		{
			InitializeComponent();

			InitializeCommand();
			InitializeState();

			txtTerritory.Text = _noneTerritory;
			btnRequestState.Enabled = false;
			btnSendCommand.Enabled = false;
		}

		private void InitializeCommand()
		{
			_commands = new List<CommandDescription>();
			_commands.Add( new CommandDescription() { Name = "Включить реле на вход \\ Открыть дверь", Code = 1 } );
			_commands.Add( new CommandDescription() { Name = "Включить реле на выход", Code = 2 } );
			_commands.Add( new CommandDescription() { Name = "Закрыть дверь", Code = 4 } );
			_commands.Add( new CommandDescription() { Name = "Установить относительную блокировку", Code = 8 } );
			_commands.Add( new CommandDescription() { Name = "Снять относительную блокировку", Code = 16 } );
			_commands.Add( new CommandDescription() { Name = "Установить абсолютную блокировку", Code = 32 } );
			_commands.Add( new CommandDescription() { Name = "Снять абсолютную блокировку", Code = 64 } );
			_commands.Add( new CommandDescription() { Name = "Установить на охрану", Code = 128 } );
			_commands.Add( new CommandDescription() { Name = "Снять с охраны", Code = 256 } );
			_commands.Add( new CommandDescription() { Name = "Включить доп. реле", Code = 512 } );
			_commands.Add( new CommandDescription() { Name = "Выключить доп. реле", Code = 1024 } );
			_commands.Add( new CommandDescription() { Name = "Сброс антипассбека", Code = 2048 } );

			comboCommand.DataSource = _commands;
			comboCommand.DisplayMember = "Name";
			comboCommand.ValueMember = "Code";
		}

		private void InitializeState()
		{
			_states = new List<StateDescription>();

			_states.Add( new StateDescription()
			{
				Name = "Аккумулятор", BitSet = "Норма", BitClear = "Разраяжен", bitIndex = 0
			} );

			_states.Add( new StateDescription()
			{
				Name = "Сетевое питание", BitClear = "Отключено", BitSet = "Норма", bitIndex = 1
			} );

			_states.Add( new StateDescription()
			{
				Name = "Батарея", BitClear = "Неисправна", BitSet = "Норма", bitIndex = 2
			} );

			_states.Add( new StateDescription()
			{
				Name = "Корпус", BitClear = "Открыт", BitSet = "Закрыт", bitIndex = 3
			} );

			_states.Add( new StateDescription()
			{
				Name = @"Реле на вход \ Замок", BitClear = @"Выключено \ Закрыт", BitSet = @"Включено \ Открыт", bitIndex = 4
			} );

			_states.Add( new StateDescription()
			{
				Name = "Реле на выход", BitClear = "Выключено", BitSet = "Включено", bitIndex = 5
			} );

			_states.Add( new StateDescription()
			{
				Name = "Доп. реле", BitClear = "Выключено", BitSet = "Включено", bitIndex = 6
			} );

			_states.Add( new StateDescription()
			{
				Name = "Реле картоприемника", BitClear = "Выключено", BitSet = "Включено", bitIndex = 7
			} );

			_states.Add( new StateDescription()
			{
				Name = "Абсолютная блокировка", BitClear = "Выключена", BitSet = "Включена", bitIndex = 8
			} );

			_states.Add( new StateDescription()
			{
				Name = "Относительная блокировка", BitClear = "Выключена", BitSet = "Включена", bitIndex = 9
			} );

			_states.Add( new StateDescription()
			{
				Name = "Экстренное открывание двери", BitClear = "Выключено", BitSet = "Включено", bitIndex = 10
			} );

			_states.Add( new StateDescription()
			{
				Name = "Охрана", BitClear = "Снята", BitSet = "На охране", bitIndex = 11
			} );

			_states.Add( new StateDescription()
			{
				Name = "Охранный датчик", BitClear = "Норма", BitSet = "Активирован", bitIndex = 12
			} );

			_states.Add( new StateDescription()
			{
				Name = @"Датчик входа \ Дверной контакт", BitClear = "Норма", BitSet = "Активирован", bitIndex = 13
			} );

			_states.Add( new StateDescription()
			{
				Name = "Датчик выхода", BitClear = "Норма", BitSet = "Активирован", bitIndex = 14
			} );

			_states.Add( new StateDescription()
			{
				Name = "Требует внимания", BitClear = "Нет", BitSet = "Да", bitIndex = 24
			} );

			_states.Add( new StateDescription()
			{
				Name = "Выключен", BitClear = "Нет", BitSet = "Да", bitIndex = 28
			} );

			_states.Add( new StateDescription()
			{
				Name = "Недоступен", BitClear = "Нет", BitSet = "Да", bitIndex = 31
			} );

			_states.Add( new StateDescription()
			{
				Name = "Неизветсное состояние", BitClear = "Нет", BitSet = "Да", bitIndex = 30
			} );


		}


		private void btnSelectTerritory_Click ( object sender, EventArgs e )
		{
			ShowTerritoryHierarhy();
		}

		private void ShowTerritoryHierarhy ()
		{
			using ( TerritoryHierarhy frm = new TerritoryHierarhy() )
			{
				if ( frm.ShowDialog( this ) != DialogResult.OK )
					return;

				Territory selectedObject = frm.SelectedTerritory;
				if ( selectedObject == null )
				{
					_territoryID = Guid.Empty;
					txtTerritory.Text = _noneTerritory;
				}
				else
				{
					_territoryID = selectedObject.ID;
					txtTerritory.Text = string.Format( "{0}", selectedObject.NAME );
				}

				btnRequestState.Enabled = ( _territoryID != Guid.Empty );
				btnSendCommand.Enabled = ( _territoryID != Guid.Empty );
			}
		}


		private void UpdateState( ulong value )
		{
			labelState.Text = value.ToString();

			listState.Items.Clear();
			if ( value != 0 )
			{
				for ( int i = 0; i < _states.Count; ++i )
				{
					var checkBit = 1 << _states[i].bitIndex;
					var bitState = ( value & (ulong)checkBit );

					var item = new ListViewItem( _states[ i ].Name );
					item.SubItems.Add( ( bitState != 0 ? _states[ i ].BitSet : _states[ i ].BitClear ) );

					listState.Items.Add( item );
				}
			}
		}

		private void btnRequestState_Click(object sender, EventArgs e)
		{
			RequestState();
		}

		private void btnSendCommand_Click( object sender, EventArgs e )
		{
			if( _territoryID == Guid.Empty )
				return;

			var command = (int)comboCommand.SelectedValue;
			if( command == 0 )
				return;

			IntegrationService integServ = new IntegrationService();
			var result = integServ.SendHardwareCommand( ClientState.SessionID, _territoryID, command );
			if( result.Result != ClientState.Result_Success )
			{
				MessageBox.Show( result.ErrorMessage, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error );
			}
			else
			{
				UpdateState( 0 );
				Thread.Sleep( 500 );
				RequestState();
			}
		}

		private void RequestState()
		{
			if( _territoryID == Guid.Empty )
				return;

			ulong state = 0;

			IntegrationService integServ = new IntegrationService();
			var result = integServ.GetHardwareState( ClientState.SessionID, new Guid[] { _territoryID } );
			if( result != null && result.Length == 1 )
			{
				if( result[ 0 ].TerritoryID == _territoryID )
					state = result[ 0 ].State;
			}
			else
			{
				MessageBox.Show( "Что-то пошло не так! См. лог файл.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error );
			}

			UpdateState( state );
		}
	}
}
