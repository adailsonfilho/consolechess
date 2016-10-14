using System;
					
public class Program
{
	
	private static Tabuleiro tabuleiro;
	private static String[] LINHAS  = {"A","B","C","D","E","F","G","H"};
		
	public static void Main()
	{
		
		Console.WriteLine("Bem vindo ao chadrez de console, =)");
		
		Console.WriteLine("Por enquanto que a skynet não chega, para dois players humanos jogarem!");
		
		tabuleiro = new Tabuleiro();
		
		bool inGame = true;
		
		/*Jogador jogador1 = PegarDadosDeJogador();
		Jogador jogador2 = PegarDadosDeJogador();*/
		
		Console.WriteLine("Vamos, lá!");
		
		while(inGame){
			
			imprimirTabuleiro();
			PegarMovimento();			
			
		}
		
		Console.WriteLine("O jogo bobou =(");
	}
	
	public static void imprimirTabuleiro(){
		
		var blankHorizontal = "  {0} ";
		var blankCell = "|{0}";
		var line = 		"----";
		
		//column ids header
		Console.Write(blankHorizontal," ");
		for(int i = 0; i < Tabuleiro.HEIGHT; i++){
			Console.Write(blankHorizontal,i.ToString());
		}
		Console.WriteLine();
		
		//inicializa configuracao
		for(int i = 0; i < Tabuleiro.WIDTH; i++){
			
			Console.Write(blankHorizontal,LINHAS[i]);
			
			for(int j = 0; j < Tabuleiro.HEIGHT; j++){
				var peca = tabuleiro.GetPeca(new Local{X=i, Y=j});
				if(peca != null){
					
					if(peca.Cor == Cores.Preta){
						
						// Console.ForegroundColor = ConsoleColor.White;
						// Console.BackgroundColor = ConsoleColor.Black;
						Console.ForegroundColor = ConsoleColor.Black;
						Console.BackgroundColor = ConsoleColor.White;
					}
					Console.Write(blankCell,peca.Descricao.Substring(0,3));
					
					// Console.ForegroundColor = ConsoleColor.Black;
					// Console.BackgroundColor = ConsoleColor.White;
					Console.ForegroundColor = ConsoleColor.White;
					Console.BackgroundColor = ConsoleColor.Black;
					
				}else{
					Console.Write(blankCell,"   ");
				}
			}
			Console.Write("|");
			
			Console.WriteLine();
			Console.Write(blankHorizontal," ");
			for(int j = 0; j < Tabuleiro.HEIGHT; j++){
				Console.Write(line);
			}
			Console.WriteLine();
			
		}
	}

	
	public static void PegarMovimento(){
		
		Local de = null;
		Local para = null;
		
		bool valido = false;
		
		Peca peca = null;
		
		do{
			var answer = "";
			Console.WriteLine("Insira de onde (ex:G-10) para onde a peça vai(ex: G-8):");
			Console.Write("De: ");
			
			answer = Console.ReadLine();
			var local = answer.Split('-');
			de = new Local{X = Array.IndexOf(LINHAS, local[0]) ,Y=Int32.Parse(local[1])};
			
			//pega peça na posicao
			peca = tabuleiro.GetPeca(de);
			
			Console.Write("Para: ");
			
			answer = Console.ReadLine();
			local = answer.Split('-');
			para = new Local{X = Array.IndexOf(LINHAS, local[0]) ,Y=Int32.Parse(local[1])};
			
			valido = true;
			
		}while(!valido);
		
		peca.Movimenta(tabuleiro, para);
		
	
	}
	
	public static Jogador PegarDadosDeJogador(){
		
		Jogador jogador = new Jogador();
		
		Console.WriteLine("Nome de jogador:");		
		jogador.Nome = Console.ReadLine();
		
		
		Console.WriteLine("Você quer ser as peças de cor Preta[P] ou Banca[B]?");
		
		var answer = "{vazio}";
		while(answer != "B" && answer != "P"){
			if(answer != "{vazio}"){
				Console.WriteLine("Mano, P ou B ... É assim tão difícil?");
			} 
			answer = Console.ReadLine();
			answer.ToUpper();
		}
		
		Console.WriteLine("Massa! Jogador registrado!");
		
		if(answer == "P")
			jogador.Cor = Cores.Preta;
		else
			jogador.Cor = Cores.Branca;
		
		return jogador;
	}
}

public class Tabuleiro{
	
	public const int WIDTH = 8;
	public const int HEIGHT = 8;
	
	private Peca[,] Configuracao{get;set;}
	
	public Tabuleiro(){
		Initialization();
	}
	
	public Peca GetPeca(Local local){
		
		return this.Configuracao[local.X, local.Y];

	}
	
	public void SetPeca(Peca peca, Local local){
		
		Local anterior = peca.Local;
		peca.Local = local;
		Configuracao[local.X, local.Y] = peca;
		Configuracao[anterior.X, anterior.Y] = null;
	}

	private void Initialization(){
		
		this.Configuracao = new Peca[WIDTH,HEIGHT];
		
		Peca[,] brancoConfig = new Peca[2,8] {
				{	new Torre{Descricao = "Torre",Cor = Cores.Branca},
					new Cavalo{Descricao = "Cavalo", Cor= Cores.Branca},
					new Bispo{Descricao = "Bispo", Cor= Cores.Branca},
					new Rei{Descricao = "Rei", Cor= Cores.Branca},
					new Rainha{Descricao = "Rainha", Cor= Cores.Branca},
					new Bispo{Descricao = "Bispo", Cor= Cores.Branca},
					new Cavalo{Descricao = "Cavalo", Cor= Cores.Branca},
					new Torre{Descricao = "Torre",Cor = Cores.Branca}
				},
				{	new Peao{Descricao = "Peão",Cor = Cores.Branca},
					new Peao{Descricao = "Peão",Cor = Cores.Branca},
					new Peao{Descricao = "Peão",Cor = Cores.Branca}, 
					new Peao{Descricao = "Peão",Cor = Cores.Branca},
					new Peao{Descricao = "Peão",Cor = Cores.Branca},
					new Peao{Descricao = "Peão",Cor = Cores.Branca},
					new Peao{Descricao = "Peão",Cor = Cores.Branca}, 
					new Peao{Descricao = "Peão",Cor = Cores.Branca}
				}
		};
		
		Peca[,] pretoConfig = new Peca[2,8] {
				{	new Peao{Descricao = "Peão",Cor = Cores.Preta},
					new Peao{Descricao = "Peão",Cor = Cores.Preta},
					new Peao{Descricao = "Peão",Cor = Cores.Preta}, 
					new Peao{Descricao = "Peão",Cor = Cores.Preta},
					new Peao{Descricao = "Peão",Cor = Cores.Preta},
					new Peao{Descricao = "Peão",Cor = Cores.Preta},
					new Peao{Descricao = "Peão",Cor = Cores.Preta}, 
					new Peao{Descricao = "Peão",Cor = Cores.Preta}
				},
				{	new Torre{Descricao = "Torre",Cor = Cores.Preta },
					new Cavalo{Descricao = "Cavalo", Cor= Cores.Preta},
					new Bispo{Descricao = "Bispo", Cor= Cores.Preta},					
					new Rainha{Descricao = "Bispo", Cor= Cores.Preta},
					new Rei{Descricao = "Bispo", Cor= Cores.Preta},
					new Bispo{Descricao = "Bispo", Cor= Cores.Preta},
					new Cavalo{Descricao = "Cavalo", Cor= Cores.Preta},
					new Torre{Descricao = "Torre",Cor = Cores.Preta}
				}				
		};
		
		//inicializa configuracao
		for(int i = 0; i < WIDTH; i++){
			for(int j = 0; j < HEIGHT; j++){
				
				if(i<2){
					brancoConfig[i,j].Local = new Local{X=i, Y=j};
					this.Configuracao[i,j] = brancoConfig[i,j];
				}else if(i>5){
					pretoConfig[i-6,j].Local = new Local{X=i, Y=j};
					this.Configuracao[i,j] = pretoConfig[i-6,j];
				}else{
					this.Configuracao[i,j] = null;									 
				}
			}
		}
	}
}

public enum Cores{
	Preta,
	Branca
}

public class Jogador{
	
	public String Nome {get;set;}
	public Cores Cor {get;set;}
		
}

public class Local{
	
	public int X {get;set;}
	
	public int Y {get;set;}
	
}

public class Bispo : Peca {
	
	private bool isFirstStep = false;
	
	public override void Movimenta(Tabuleiro tabuleiro, Local para){
		
		if(isFirstStep){	
			
		}else{
			
		}
	}	

}

public class Cavalo : Peca {
	
	private bool isFirstStep = false;
	
	public override void Movimenta(Tabuleiro tabuleiro, Local para){
		
		if(isFirstStep){	
			
		}else{
			
		}
	}	

}

public class Rainha : Peca {
	
	private bool isFirstStep = false;
	
	public override void Movimenta(Tabuleiro tabuleiro, Local para){
		
		if(isFirstStep){	
			
		}else{
			
		}
	}	

}

public class Rei : Peca {
	
	private bool isFirstStep = false;
	
	public override void Movimenta(Tabuleiro tabuleiro, Local para){
		
		if(isFirstStep){	
			
		}else{
			
		}
	}	

}

public class Torre : Peca {
	
	private bool isFirstStep;
	
	public override void Movimenta(Tabuleiro tabuleiro, Local para){
		
		if(isFirstStep){	
			
		}else{
			
		}
	}	

}

public class Peao : Peca  {
	
	private bool isFirstStep = true;
	
	public override void Movimenta(Tabuleiro tabuleiro, Local para){
			
		var paraPeca = tabuleiro.GetPeca(para);
		
		if(paraPeca == null){//celula vazia			
			
			if(para.Y == this.Local.Y){
				
				if(isFirstStep && Math.Abs(this.Local.X-para.X) <= 2){
					
					tabuleiro.SetPeca(this, para);
					isFirstStep = false;
					
				}else if(Math.Abs(this.Local.X-para.X) == 1){
					
					tabuleiro.SetPeca(this, para);
				}
				
			}else{
				Console.WriteLine("Tu qer ir pra onde boy? Pode, não, vai estudar chadrez!!");
			}
			
		}else{//celula com peca
			if(this.IsEnemy(paraPeca)){
				
				if(Math.Abs(this.Local.X-para.X) == 1 && Math.Abs(this.Local.Y-para.Y) == 1){
					tabuleiro.SetPeca(this, para);
				}else{
					Console.WriteLine("Tu qer ir pra onde boy? Pode, não, vai estudar chadrez!!");
				}
				
			}else{
				Console.WriteLine("EEIITAAA!!Tu quer ir pra cima do coleguinha, rapaz? Pode não!");
			}		
		}
			
	}
	
}

public abstract class Peca {
	
	public String Descricao {get;set;}
	
	public Cores Cor {get;set;}
	
	public bool IsEnemy(Peca peca){
		return this.Cor != peca.Cor;
	}
	
	public String ImageURL {get;set;}
	
	public Local Local{get;set;}
	
	public abstract void Movimenta(Tabuleiro tabuleiro, Local para);
	
}
