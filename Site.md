Inicialmente a aplicação será para apnas uma distribuidora

INÍCIO

Página Principal
	Pequeno resumo sobre a função do programa e um botão REGISTRE-SE
	Cabeçalho já tem o longin e senha de acesso


Cadastro
	Nome
	Se é física ou jurídica
		Cpf
		CNPJ
	e-mail
	Senha
	confirmação de senha
	BOTÃO: Confirma


Depois do login, no cabeçalho haverá seu nome e um botão DESCONECTAR	

Perfil
	Dados básicos
		Botão para alterar dados do usuário (Criar uma tela para alteração desses dados)
	Simulações salvas (lista)
		Informações das simulações
			nome
			Potência
			barra
			
		Botão ao lado de cada uma: Complementar dados e solicitar acesso
			Ação do Botão: Página de complemento de dados para solicitção de acesso
			
		Botão imprimir com informações detalhadas
		
	Botão de criar nova simulação
		Ação do botão: Ir para a página de simulação
		
	Botão "imprimir todas" com informações detalhadas de cada uma
	
Página de altração de dados básicos
	Dados do cadastro
	Caixas de preenchimento

Simulador
	
	Na esquerda
		Dados de simulação
			Nome do projeto
			Potência dos Pontos de recarga
				Caixa para preencher (kW)					Caixa [inicialmente apagada até o preenchimento da anterior] para preencher (kW)
				Caixa para preencher (Quantidade)			Caixa [inicialmente apagada até o preenchimento da anterior] para preencher (Quantidade)
				Total (kW * Qt)								Total (kW * Qt)																				Total final (soma dos totais)
			
			Terreno previsto para eletroposto
				apontar no mapa
					ativar o mouse para identificar o endereço quando clicar no mapa e preencher automaticamente o endereço
				preencher endereço
					cep
					logradouro
					número
				Procurar postes num raio de:
					Caixa para preencher (m)
					
			BOTÃO: Gerar Simulação
			
			Especicações da barra selecionada (invisível até ser escolhida)
				Tensão de fornecimento
				Sistema de fornecimento
				Nível de Curto-circuito
				
	
	Na direita
		Mapa com a rede
			Ao clicar em apontar no mapa
				permitir o clique para identificação de endereço
			Após clique ou preenchimento do endereço
				gerar um raio de 600m ao redor do endereço especificado
					apresentar os postes coloridos
						de acordo com a porcentagem de carga da linha incluindo a carga do eletroposto solicitado/poste
							ao passar o mouse no poste abrir uma tooltip com:
								% de carregamento (base em corrente)
								Tensão calculada
								Corrente nominal
								Corrente calculada
								
							pensar num algoritimo
						
						Selecionar barra desejada
							Especificações abrem na esquerda abaixo do endereço









Solicitação de Acesso (cadastro para pedido viabilidade técnica após a simulação)