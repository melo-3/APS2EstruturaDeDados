# Sistema de Cadastro e Gerenciamento de Pacientes com Tabela Hash

**Grupo:** Eduardo Paes, Giovanna de Melo, Mariana Alves e Nicole Assis

---

## Descrição

Este projeto simula o atendimento de pacientes em um sistema de saúde, aplicando conceitos avançados de estruturas de dados em C#. Ele utiliza **tabela hash** (com CPF como chave única), listas, filas e pilhas para organizar, cadastrar e gerenciar pacientes de forma eficiente. O objetivo é demonstrar, de forma prática, como diferentes estruturas de dados trabalham juntas para resolver problemas reais de organização, busca e atendimento prioritário.

---

## Cenário Proposto

O sistema representa todo o fluxo de atendimento, desde o cadastro do paciente, triagem, classificação de prioridade até o atendimento clínico, utilizando uma tabela hash para busca rápida e eficiente por CPF, simulando um ambiente clínico realista.

### Etapas do Atendimento

1. **Cadastro do Paciente:**  
   O paciente é cadastrado com os seguintes dados:
   - CPF (chave única)
   - Nome completo
   - Pressão arterial
   - Temperatura corporal
   - Nível de oxigenação

2. **Classificação de Prioridade:**  
   Com base nos dados clínicos, o sistema atribui automaticamente uma prioridade:
   - **Vermelha (Prioridade Máxima):** pressão arterial > 18, temperatura > 39°C ou oxigenação < 90%
   - **Amarela (Prioridade Média):** sinais alterados, fora dos critérios críticos (pressão > 14, temperatura > 37.5°C ou oxigenação < 95%)
   - **Verde (Prioridade Normal):** sinais dentro dos parâmetros normais

3. **Triagem e Fila:**  
   O paciente cadastrado é colocado em uma fila de triagem.

4. **Atendimento Clínico:**  
   Os pacientes são atendidos por ordem de prioridade (vermelha, amarela e verde).

5. **Histórico:**  
   Um histórico dos pacientes atendidos é mantido utilizando uma pilha (stack).

---

## Funcionalidades

- **Inserção de paciente** na tabela hash (utilizando CPF como chave única).
- **Busca rápida** de paciente pelo CPF.
- **Atualização de dados clínicos** de um paciente já cadastrado.
- **Remoção de paciente** do cadastro.
- **Exibição completa da tabela hash** mostrando os buckets e colisões.
- **Fila de triagem e atendimento** priorizado conforme classificação.
- **Histórico de atendimentos** utilizando pilha.
- **Menu interativo** no terminal para acesso contínuo às funcionalidades, sem reinicialização.

---

## Estruturas de Dados Utilizadas

- **Tabela Hash** (com encadeamento separado): armazenamento e gerenciamento eficiente dos pacientes, com CPF como chave.
- **Filas (`Queue`)**: simulação da ordem de chegada/triagem e filas por prioridade.
- **Pilha (`Stack`)**: manutenção do histórico de atendimentos realizados.
- **Listas/Arrays**: exibição geral e suporte às operações.
- (Opcional) **Matriz**: para armazenamento dos dados clínicos, se desejado para expansão.

---

## Organização do Código

O projeto está organizado em classes coesas:

- **Paciente:**  
  Armazena informações do paciente, dados clínicos e lógica de prioridade.

- **TabelaHash:**  
  Implementa a tabela hash genérica, com CPF como chave e tratamento de colisões via encadeamento.

- **SistemaClinico:**  
  Gerencia o cadastro, busca, atualização, remoção, triagem, atendimento e histórico dos pacientes.

- **Program:**  
  Classe principal com o menu interativo para o usuário.

---

## Como Executar

1. Clone este repositório.
2. Abra o projeto em um ambiente compatível com C# (.NET).
3. Compile e execute o programa.
4. Siga as instruções no terminal para cadastrar pacientes, buscar, atualizar, remover e visualizar a tabela hash e o histórico de atendimentos.

---

## Exemplo de Funcionamento

=== MENU ===
1. Cadastrar paciente
2. Buscar paciente por CPF
3. Atualizar dados clínicos de paciente
4. Remover paciente
5. Exibir tabela hash
6. Realizar triagem e atendimento
7. Exibir histórico de atendimentos
0. Sair
Escolha uma opção: 1

CPF: 12345678900
Nome completo: Maria Silva
Pressão arterial: 19
Temperatura corporal (°C): 37
Nível de oxigenação (%): 95
Paciente cadastrado e adicionado à fila de triagem.

...

--- Triagem e Atendimento ---
12345678900 | Maria Silva | PA: 19 | Temp: 37°C | O2: 95% | Prioridade: Vermelha
Atendendo: 12345678900 | Maria Silva | PA: 19 | Temp: 37°C | O2: 95% | Prioridade: Vermelha

--- Histórico de Atendimentos ---
Maria Silva (12345678900) - Prioridade: Vermelha

---

## Recomendações e Diferenciais

- **Cores no terminal** indicam visualmente a prioridade do paciente (vermelho, amarelo, verde).
- O código é totalmente orientado a objetos e modularizado.
- O histórico de atendimentos é apresentado ao final, utilizando pilha.
- Tabela hash com capacidade fixa (por exemplo, 10 posições) para facilitar a visualização de colisões.
- Simulações podem ser feitas com dados que provoquem colisões, demonstrando o funcionamento do encadeamento.
- Fácil de expandir para interface gráfica ou integração com banco de dados.

---

## Objetivo Final

Demonstrar, por meio de uma simulação realista, como o uso de estruturas de dados modernas (tabela hash, filas, pilhas) contribui para a organização, priorização e eficiência no atendimento de pacientes em sistemas de saúde.

---

## Licença

Este projeto é apenas para fins acadêmicos/didáticos.
