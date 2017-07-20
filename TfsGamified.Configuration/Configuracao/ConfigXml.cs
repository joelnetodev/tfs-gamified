using System;
using System.Collections.Generic;
using System.Xml;

namespace TfsGamified.Configuration.ConfigXml
{
    public sealed class ConfigXml : IDisposable
    {
        private XmlDocument doc;
        
        private bool ConfiguracoesCarregadas { get; set; }

        public List<string> LoginsDesenvolvedores(string nomeProjeto)
        {
            XmlNode node = doc.DocumentElement.SelectSingleNode(nomeProjeto.ToLower());
            node = node.SelectSingleNode("loginsDesenvolvedores");

            return new List<string>(node.InnerText.Split(','));
        }

        public List<string> LoginsAtivos(string nomeProjeto)
        {

            XmlNode node = doc.DocumentElement.SelectSingleNode(nomeProjeto.ToLower());
            node = node.SelectSingleNode("loginsAtivos");

            return new List<string>(node.InnerText.Split(','));
        }

        public string CampoEstimadoTarefa { get; private set; }
        public string CampoRealizadoTarefa { get; private set; }
        public string CampoEstimadoProblema { get; private set; }
        public string CampoRealizadoProblema { get; private set; }

        public int PontuacaoAtividade { get; private set; }
        public int PontuacaoAtividadeReaberta { get; private set; }
        public int PontuacaoAtividadePerdida { get; private set; }

        public int PontuacaoSuporte { get; private set; }
        public int PontuacaoHalter { get; private set; }
        public int PontuacaoCerteiro { get; private set; }
        public int PontuacaoAjudante { get; private set; }
        public int PontuacaoSabio { get; private set; }

        public int QuantidadePremioTarefa { get; private set; }
        public int QuantidadePremioProblema { get; private set; }
        public int PontuacaoPremioTarefa { get; private set; }
        public int PontuacaoPremioProblema { get; private set; }

        public ConfigXml()
        {
            try
            {
                ConfiguracoesCarregadas = false;
                doc = new XmlDocument();
                doc.Load(AppDomain.CurrentDomain.BaseDirectory + @"bin\Configuracao\configfile.xml");

                CarregarConfiguracaoXml();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao obter configurações XML - " + ex.Message);
            }          
        }

        public void CarregarConfiguracaoXml()
        {
            if (ConfiguracoesCarregadas) return;

            ConfiguracoesCarregadas = true;

            XmlNode node = doc.DocumentElement.SelectSingleNode("campoEstimadoTarefa");
            CampoEstimadoTarefa = node.InnerText;
            node = doc.DocumentElement.SelectSingleNode("campoRealizadoTarefa");
            CampoRealizadoTarefa = node.InnerText;
            node = doc.DocumentElement.SelectSingleNode("campoEstimadoProblema");
            CampoEstimadoProblema = node.InnerText;
            node = doc.DocumentElement.SelectSingleNode("campoRealizadoProblema");
            CampoRealizadoProblema = node.InnerText;


            node = doc.DocumentElement.SelectSingleNode("pontuacaoAtividade");
            PontuacaoAtividade = Convert.ToInt32(node.InnerText);
            node = doc.DocumentElement.SelectSingleNode("pontuacaoAtividadeReaberta");
            PontuacaoAtividadeReaberta = Convert.ToInt32(node.InnerText);
            node = doc.DocumentElement.SelectSingleNode("pontuacaoAtividadePerdida");
            PontuacaoAtividadePerdida = Convert.ToInt32(node.InnerText);

            node = doc.DocumentElement.SelectSingleNode("pontuacaoSuporte");
            PontuacaoSuporte = Convert.ToInt32(node.InnerText);
            node = doc.DocumentElement.SelectSingleNode("pontuacaoHalter");
            PontuacaoHalter = Convert.ToInt32(node.InnerText);
            node = doc.DocumentElement.SelectSingleNode("pontuacaoCerteiro");
            PontuacaoCerteiro = Convert.ToInt32(node.InnerText);
            node = doc.DocumentElement.SelectSingleNode("pontuacaoAjudante");
            PontuacaoAjudante = Convert.ToInt32(node.InnerText);
            node = doc.DocumentElement.SelectSingleNode("pontuacaoSabio");
            PontuacaoSabio = Convert.ToInt32(node.InnerText);
            

            node = doc.DocumentElement.SelectSingleNode("quantidadePremioTarefa");
            QuantidadePremioTarefa = Convert.ToInt32(node.InnerText);
            node = doc.DocumentElement.SelectSingleNode("quantidadePremioProblema");
            QuantidadePremioProblema = Convert.ToInt32(node.InnerText);
            node = doc.DocumentElement.SelectSingleNode("pontuacaoPremioTarefa");
            PontuacaoPremioTarefa = Convert.ToInt32(node.InnerText);
            node = doc.DocumentElement.SelectSingleNode("pontuacaoPremioProblema");
            PontuacaoPremioProblema = Convert.ToInt32(node.InnerText);
        }

        public void Dispose()
        {
            ConfiguracoesCarregadas = false;
            doc = null;
        }
    }
}
