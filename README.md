# Desafio Técnico
> Vaga: Desenvolvedor .NET | .NET Core - Pleno

### ⚡ PROBLEMA
#### O Software criado têm a proposta de solucionar um GRANDE PROBLEMA:
Afinal, é comum perdermos tempo escolhendo onde iremos almoçar. ⏰<br>
E não gostaríamos mais de perder vários minutos escolhendo um restaurante, e no final sempre chatear algum colega por não almoçar onde gostaria ou até mesmo sugerir um local.  🔪 💣 <br>

### 📲 SOLUÇÃO
##### Eu como um facilitador do processo de votação, criei um sistema de votação onde você pode votar no seu Restaurante Favorito: <br>
Cada Profissional faminto poderá votar em um RESTAURANTE por dia. <br>
Mas não fique triste caso seu Restaurante favorito não seja escolhido, porquê cada restaurante só poderá ser escolhido uma vez na semana! 😣 <br>
Então, todo dia, de segunda à sexta(por padrão) a votação é aberta às 07:00 a.m e encerrada às 11:30 a.m. <br>
Neste horário, 11:30 você também saberá qual foi o Restaurante escolhido e poderá preparar-se psicologicamente para o almoço. <br>

#### ⚠ Mas.. e se der empate??
Fique tranquilo, porquê o sistema irá sortear entre os mais votados de forma que sempre haverá um lugar para almoçar! <br>


### 🚧 QUAIS SÃO OS REQUISITOS DO SISTEMA?
SDK do .Net Core versão 3.1.302 <br>

### 🎮 Como utilizar?
- Cada Profissional faminto poderá cadastrar seus Restaurantes favoritos no sistema, automaticamente cada restaurante cadastrado poderá ser votado em qualquer votação, inclusive nas em andamento. <br>
- Cada Profissional faminto poderá votar em apenas um restaurante por dia. <br>
- Cada Restaurante por sua vez, sendo escolhido, só poderá concorrer na próxima semana/votação(cada votação tem um período de 5 dias). <br>
- Após o horário de término, os profissionais poderão realizar uma consulta no sistema e então verificar o restaurante escolhido! <br>

### 📝 MELHORIAS
Há algumas melhorias que podem ser feitas, tais como: 
- Implementar autenticação dos Profissionais, afim de evitar fraudes na votação;  
- Implementar outro tipo de decisão quando a votação terminar em EMPATE, abrindo mão da abordagem atual de SORTEIO;

#### 🏆 CONSIDERAÇÕES FINAIS
Vale ressaltar que a decisão de utilização da abordagem DDD e de CQRS na implementação se deve a: <br>
- Concentração de regras de negócio no Domínio; <br>
- Possibilitar o consumo do Sistema através de diferentes aplicações: API, Console e etc; <br>
- Facilitar os Testes; <br>

Melhorias e sugestões são sempre bem-vindas! 👊 🚀
