BasicInterpreter_Dotnet8
Um interpretador BASIC simples em C# com .NET
Este repositório contém um interpretador BASIC simples escrito em C# com .NET. O interpretador atualmente suporta:

-Atribuições de variáveis
-Operações aritméticas básicas (+, -, *, /)
-Impressão de mensagens na tela

Funcionalidades ainda não implementadas:

-GOTO
-HALT
-END
-IF
-Laços de repetição (FOR, WHILE)


Como usar
Para usar o interpretador, siga estas etapas:

Clone o repositório para o seu computador.
Abra a solução BasicInterpreter.sln no Visual Studio.
Compile a solução.
Altere o program.cs com seu código BASIC.
Exemplo:
Basic
LET x = 10
                LET y = x + 2 * 3
                PRINT ""A soma de x e y �: ""
                PRINT y + x
            ";

