

using CommunityToolkit.Mvvm.ComponentModel;
using SQLite; 
using System;

namespace CadastroEventosMAUI.Models
{
    public class EventoModel : ObservableObject
    {

        [PrimaryKey, AutoIncrement] 
        public int Id { get; set; }

        private string _nome = string.Empty;
        private DateTime _dataInicio = DateTime.Today;
        private DateTime _dataTermino = DateTime.Today.AddDays(1);
        private int _numeroParticipantes;
        private string _local = string.Empty;
        private decimal _custoPorParticipante;


        public string Nome
        {
            get => _nome;
            set => SetProperty(ref _nome, value);
        }

        public DateTime DataInicio
        {
            get => _dataInicio;
            set
            {
                if (SetProperty(ref _dataInicio, value))
                {
                    OnPropertyChanged(nameof(Duracao));
                    OnPropertyChanged(nameof(DuracaoEmDias));
                }
            }
        }

        public DateTime DataTermino
        {
            get => _dataTermino;
            set
            {
                if (SetProperty(ref _dataTermino, value))
                {
                    OnPropertyChanged(nameof(Duracao));
                    OnPropertyChanged(nameof(DuracaoEmDias));
                }
            }
        }

        public int NumeroParticipantes
        {
            get => _numeroParticipantes;
            set
            {
                if (SetProperty(ref _numeroParticipantes, value))
                {
                    OnPropertyChanged(nameof(CustoTotal));
                }
            }
        }

        public string Local
        {
            get => _local;
            set => SetProperty(ref _local, value);
        }

        public decimal CustoPorParticipante
        {
            get => _custoPorParticipante;
            set
            {
                if (SetProperty(ref _custoPorParticipante, value))
                {
                    OnPropertyChanged(nameof(CustoTotal));
                }
            }
        }


        public TimeSpan Duracao => DataTermino.Subtract(DataInicio);
        public double DuracaoEmDias => Duracao.TotalDays;
        public decimal CustoTotal => CustoPorParticipante * NumeroParticipantes;


    }
}
