# POO II

### 🚩 Orientação de Desenvolvimento da Atividade

<img src="./assets/img/orientacao.png" alt="orientacao">

### 🚩 Cenário 03

<img src="./assets/img/cenario03.png" alt="cenario02">

## 💻 Desenvolvimento da Atividade

<h3>Classes</h3>

    1. CLIENTE

    2. TECNICO

    3. ORDEMSERVICO

    4. TIPOSERVICO: Manutencao, Instalacao, Suporte

    5. EXECUCAOSERVICO

    6. PAGAMENTO
    subclasses: PagamentoCartao, PagamentoPix

    7. HISTORICO


<h3>Métodos</h3>

    1. CLIENTE
    abrirOrdem(detalhes) -> OrdemServico

    2. TECNICO
    aceitarOrdem(os), executarOrdem(os, execucao)

    3. ORDEMSERVICO
    atribuirTecnico(tecnico)
    iniciar()
    finalizar(valorFinal)
    adicionarObservacao()
    calcularValor()

    4. TIPOSERVICO
    obterPrecoBase()
    obterProcedimento()

    5. EXECUCAOSERVICO
    registrarPasso()
    anexarEvidencia()

    6. PAGAMENTO
    pagar(valor)
    validar()

<h3>Interfaces</h3>

        IServico (interface para tipos de serviço)
        calcularPreco(parametros): decimal
        executarProcedimento(dados): ResultadoExecucao

        IOrdemServicoRepository (persistência)
        salvar(os)
        buscar(id)

<h3>Relacionamentos</h3>

        Cliente 1..* OrdemServico onde o cliente pode realizar a solicitação de ordem de serviços
        OrdemServico tem 1 Tecnico após a atribuição
        OrdemServico tem 1 TipoServico
        OrdemServico associa ExecucaoServico e Pagamento/Historico

<h2>Tratamento de Exceções</h2>

1. Abertura de OS: validar dados do cliente e escopo como erros tratados na camada de serviço.

2. Atribuição/Execução: conflitos de agenda, tecnico indisponível onde deve capturar e comunicar.

3. Finalização: divergência de preço ou falha em validação do serviço onde deve lançar exceções e criar fluxo de revisão.

4. Persistência e integração (notificações, cobrança): tratamento com transações e compensações.
