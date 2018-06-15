using Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WFA
{
    public partial class ListaCarro : Form
    {
        string nomeAntigo = string.Empty;
        List <Carro> carros = new List<Carro>();

        public ListaCarro()
        {
            InitializeComponent();
        }

        private void btnAdicionar_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 1;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            LimparCampos();
        }

        private void LimparCampos()
        {
            txtNome.Text = "";
            txtMarca.Text = "";
            txtAnoFabricacao.Text = "";
            txtValor.Text = "";
            
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                Carro carro = new Carro()
                {
                    Nome = txtNome.Text,
                    Marca = txtMarca.Text,
                    AnoFabricacao = Convert.ToInt16(txtAnoFabricacao.Text),
                    Valor = Convert.ToDecimal(txtValor.Text)
                };
                if (nomeAntigo == "")
                {
                    carros.Add(carro);
                    MessageBox.Show("Cadastrado com Sucesso");
                    AdicioCarroATabela(carro);
                }
                else
                {
                    // retorna a possição que está o carro antigo ele busca quando o nome for igual ao nome antigo
                    int linha = carros.FindIndex(x => x.Nome == nomeAntigo);
                    carros[linha] = carro;
                    EditarCarroNaTabela(carro, linha);
                    MessageBox.Show("Alterado com Successo");
                    nomeAntigo = string.Empty;
                }
                
                LimparCampos();                
                tabControl1.SelectedIndex = 0;
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.Message);
            }            
        }

        private void EditarCarroNaTabela(Carro carro, int linha)
        {
            dataGridView1.Rows[linha].Cells[0].Value = carro.Nome;
            dataGridView1.Rows[linha].Cells[1].Value = carro.Marca;
            dataGridView1.Rows[linha].Cells[2].Value = carro.AnoFabricacao;
            dataGridView1.Rows[linha].Cells[3].Value = carro.Valor;
        }

        
       
        private void AdicioCarroATabela(Carro carro)
        {
            dataGridView1.Rows.Add(new Object[]{
                carro.Nome, carro.Marca, carro.AnoFabricacao, carro.Valor
            });
        }

        private void btnApagar_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count == 0)
            {
                MessageBox.Show("Cadastre um carro");
                tabControl1.SelectedIndex = 1;
                return;
            }

            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("Selecione uma linha");
                return;
            }

            int linhaSelecionada = dataGridView1.CurrentRow.Index;
            string nome = dataGridView1.Rows[linhaSelecionada].Cells[0].Value.ToString();

            /*Resume o for 
              carros.Remove(carros.Find(x => x.Nome == nome));*/
            for (int i = 0; i < carros.Count; i++)
            {
                Carro carro = carros[i];
                if (carro.Nome == nome)
                {
                    carros.RemoveAt(i);
                }
            }

            dataGridView1.Rows.RemoveAt(linhaSelecionada);


        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count == 0)
            {
                MessageBox.Show("Cadastre um Carro");
                tabControl1.SelectedIndex = 1;
                return;
            }

            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("Selecione uma linha");
                return;
            }

            int linhaSelecionada = dataGridView1.CurrentRow.Index;
            string nome = dataGridView1.Rows[linhaSelecionada].Cells[0].Value.ToString();
            foreach (Carro carro in carros)
            {
                if (carro.Nome == nome)
                {
                    txtNome.Text = carro.Nome;
                    txtMarca.Text = carro.Marca;
                    txtAnoFabricacao.Text = Convert.ToString(carro.AnoFabricacao);
                    txtValor.Text = Convert.ToString(carro.Valor);
                    nomeAntigo = carro.Nome;
                    tabControl1.SelectedIndex = 1;
                    break;
                }
            }
        }

      

        
    }
}
