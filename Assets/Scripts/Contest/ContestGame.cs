using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

[RequireComponent(typeof(ContestController))]
public class ContestGame : MonoBehaviour {

	private static Dictionary<string, IContestData> contests;
    private static Dictionary<string, GameObject> characters;

    private static Dictionary<string, RotationAndScale> specificTransforms;
	private static ContestGame main;
	private ContestController controller;

	// Use this for initialization
	void Awake ()
    {
        controller = GetComponent<ContestController>();

        specificTransforms = new Dictionary<string, RotationAndScale>();
        contests = new Dictionary<string, IContestData>();
        characters = new Dictionary<string, GameObject>();

        AddCdeCienciaJeffry();
        AddCrespoEinstein();
        AddAntroporamaBrain();
        AddDateUnSquirtle();
        AddMinutePhysics();
        //AddUstedEstaAqui(); //Hacer
        AddDotCSV();  
        AddRobotDePlaton(); 
        AddRobotDeColon();
        AddJaimeAltozano(); 
        AddDrMilkScience();
        AddVsauce(); 
        AddRobotDePlaton2();
        AddMundoDesconocido();

        main = this;
    }

    private void AddDrMilkScience()
    {
        contests.Add("DrMilkScience", new ContestData(
            "¿Cuál es el animal más grande del mundo? (del reino animalia y contando su largo)",
            new string[] { "Laminarias", "El gusano cordón de Bota", "La ballena azul" },
            new string[][] { 
                new string[] { 
                "Las laminarias son un género de algas que pueden alcanzar los 40 metros de altura",
                "y en casos excepcionales puede llegar a los 120 metros de altura",
                "pero las laminarias no pertenecen al reino animalia, sino al protista",
                "por lo que no es la respuesta correcta"
                },
                new string[] { 
                "¡Elemental mi querido WikiSapiens!"
                },
                new string[] { 
                "La ballena azul es el vertebrado más grande de todos los tiempos",
                "Con sus 27 metros de media es mayor incluso que el más grande de los dinosaurios",
                "Pero no quiere decir que sea el animal más grande",
                "ya que los animales no tienen por qué ser vertebrados"
            } },
            1,
            new string[]{
        "Hola soy el Dr. Milk Science, Doctor en Ciencias Chocolatina y amigo de Wikiseba",
        "Estamos cerca de la Wikiselva, donde los memes rompen la realidad",
        "Estoy investigando su increíble flora y fauna",
        "Algunas plantas como los árboles lechuga son extremadamente grandes",
        "Esperemos que no haya animales del mismo tamaño",
        "Por cierto, me podrías decir...",
                },
                new string[]{
        "Los gusanos de cordón de bota pueden alcanzar los 50 metros de largo",
        "es una especie de nemertino que apenas llega al centímetro de anchura",
        "Estos gusanos viven en las costas y son muy flexibles",
        "y es gracias a esta característica pueden conseguir ser tan largos",
        "Y siguiendo con el tema de los más grandes, vamos a invitar",
        "al canal de divulgación más grande del mundo",
        "Es lo que tiene la Corporación Wikiseba, tenemos contactos",
        "Ya sabes, busca a Vsauce",
        "PD: No quites la gran canción \"WikiSeba te Contesta\"",
        "¡Adios Wikisapiens!",
                }));
        contests["DrMilkScience"].OnEnd(DefaultOnEnd("DrMilkScience", "Vsauce",
             "Busca a Vsauce"));
        
        specificTransforms.Add("DrMilkScience", new RotationAndScale{
            rotation = Quaternion.identity,
            offset = Vector3.up * 0.0f,
            scale = Vector3.one * 0.5f
        });
    }

    private void AddVsauce()
    {
        ContestDefault contest = new ContestDefault((ContestDefault c) => { return c.WasAnswerCorrect(1); });

        Sentence firstDialog = Sentence.CreateDialog(new string[]{
                 "¡Hey Vsauce, Michael's head here!",
                 "But what is here?",
                 "¿CdeCountry? ¿Quizás el final del juego?",
                 "¿¿Debería de ser yo entonces un boss final??",
                 "Según has ido avanzando tus fallos han hecho que personas se vayan",
                 "Es una pena, pero no te preocupes ahora por ellos",
                 "De nuestros errores aprendemos más que de nuestros aciertos",
                 "Con cada acierto y fallo seguimos avanzando y experimentando",
                 "Y es solo gracias a la perseverancia y la determinación que conseguimos alcanzar nuestras metas",
                 "Al fin y al cabo, gracias a ellas has llegado hasta aquí",
                 "Gracias a ellas has terminado el juego y aprendido alguna cosa que no sabrías",
                 "Pero fracasarás en el intento si te falta una cosa",
                 "Pasión",
                 "Y no me refiero a CdeCrespo",
                 "Pasión por lo que haces, por tus sueños y ambiciones",
                 "Es esa pasión la que hará que todos esos amigos que se fueron vuelvan",
                 "Porque es lo que nos une a todos, la pasión por el conocimiento y el alcanzar la verdad",
                 "Algo que trascenderá, y con lo que seguiremos existiendo gracias a la huella que hemos dejado",
                 "Sin querer hemos creado una comunidad unida por nuestros intereses",
                 "Repartida por el mundo y, sin embargo, a la vez unida",
                 "Esto es CdeCountry, el rincón donde todos nos encontramos"
                });

        Sentence finalDialog = Sentence.CreateDialog(new string[]{
			"Ya puedes volver a CdeCountry y disfrutar de las vistas",
            "Espero que te haya gustado",
            "And as always",
            "Thanks for playing"
		});

        IContestSentence question = new ContestQuestion(contest, "Esto es solo una beta así que, ¿qué opinas?",
            new string[] { "Hay un error en el juego", "Me gustaría añadir algo", "El final es un poco ñoño pero está bien" },
             new bool[] { true, true, true }, 
             new IContestSentence[]{
                Sentence.CreateDialog(new string[]{
					"Lo siento, este proyecto está hecho por solo una persona y es normal que haya errores",
					"Manda un MD en Twitter a @100cyCdeCountry o twitealo directamente",
					"Intentaré solucionarlo lo antes posible.",
					"¡Y gracias de antemano por avisar!",
				}, finalDialog),
				Sentence.CreateDialog(new string[]{
					"Me alegro de que quieras añadir más cosas",
					"Puedo añadir más divulgadores, aunque depende de cuanto me lo pidáis",
					"Sí me enviáis los diálogos con las preguntas puedo implementarlo relativamente rápido",
                    "El juego está en un repositorio público de GitHub, por lo que si sabes de Unity puedes hacer cambios",
                    "Forkea el repositorio y luego pídeme un pull request y lo añadiré",
                    "El repositorio es https://github.com/100cyCdeCountry/CoogleMapsImplementation",
                    "Para cualquier duda o sugerencia, mándenme un MP a twitter o GitHub y os responderé",
				}, finalDialog),
				Sentence.CreateDialog(new string[]{
					"Sé que ha sido un poco ñoño, pero ya estáis acostumbrados a CdeCrespo",
					"Es una pena que no me haya dado tiempo para meter a más divulgadores",
					"De todo el proyecto lo que más me ha costado ha sido hacer los diálogos,",
                    "comprobar que las preguntas estén bien formuladas y corregidas",
                    "revisar los videos para imitar como hablan…",
                    "Si queréis ayudarme con eso estaría encantado"
				}, finalDialog)
             }
             );
    
        firstDialog.GetLast().SetNext(question);

        contest.SetSentences(firstDialog);
        contest.OnEnd((IContestData c) => { 
            UITip.SetTip("Adora a CdeCrespo");
            foreach(var character in characters)
                character.Value.GetComponent<ContestInitiator>().Show(); 

            PlayerPrefs.SetString("LevelComplete", "Beta");
            PlayerPrefs.Save();
        });
        contests.Add("Vsauce", contest);

        specificTransforms.Add("Vsauce", new RotationAndScale{
            rotation = Quaternion.identity,
            offset = Vector3.up * 2.5f,
            scale = Vector3.one
        });
    }

    private static void AddJaimeAltozano()
    {     
        contests.Add("JaimeAltozano", new ContestData(
            "¿Qué es una fuga?",
            new string[] { "Cuando Martí te roba el corazón y sale corriendo",
             "Un género musical",
              "Una superposición de voces polifónicas que \"se persiguen\"" },
            new string[][] { 
                new string[] { 
                "...",
                "Martí, ¿por qué te fugaste cuando te desvelé mis sentimientos?",
                "Tanto amas a Crespo...",
                "...",
                "Bueno, mejor te explico qué es una fuga en el sentido musical"
                },
                new string[] { 
                "Una fuga no es un género musical como el Rock o el Pop",
                },
                new string[] { 
                "En pocas palabras, eso es"
            } },
            new bool[]{true, false, true},
            new string[]{
        "Buenas, soy Jaime Altozano",
        "Soy el creador del Himno Nacional de CdeCountry y tengo un canal de divulgación de música",
        "Podría haber traído a Miku pero él no quería hacer otro cameo",
        "Y como dejar solo una ficha de apalabrados iba a ser un poco raro...",
        "Así que aquí estoy yo, con la ficha de apalabrados y mi teclado",
        "Ya que estoy, voy a hacerte una pregunta"
                },
                new string[]{
        "Una fuga es un procedimiento musical en el que varias voces se superponen",
        "Estas voces se fugan, persiguen, dialogan unas con otras, repiten temas…",
        "Como es difícil explicar algo sin escucharlo pásate por mi vídeo De Pokémon a Bach",
        "* Transición en la que debería salir Miku *",
        "Y ahora tendrás que adentrarte en la Wikiselva",
        "Pues ahí está el invitado de Wikiseba",
        "Hasta la próxima"
                }));
        contests["JaimeAltozano"].OnEnd(DefaultOnEnd("JaimeAltozano", "DrMilkScience",
             "Busca al Dr. Milk Science"));
        
        specificTransforms.Add("JaimeAltozano", new RotationAndScale{
            rotation = Quaternion.identity,
            offset = Vector3.up * 2.0f,
            scale = Vector3.one
        });
    }

    private static void AddDotCSV()
    {
        contests.Add("Sophia", new ContestData(
                    "¿Existen actualmente IA fuertes?",
                     new string[] { "Solo Sophia", "No", "Sí" },
                     new string[][] { 
                         new string[] { 
                         "Por supuesto, aunque eso es lo que me han programado que diga",
                         "Así que no es la correcta..."
                         },
                         new string[] { 
                         "Exacto"
                         },
                         new string[] { 
                         "Está bien que seas optimista...",

                     } },
                     1,
                     new string[]{
                 "Hola soy Sophia, el amor platónico de Dot.CSV",
                 "Soy un robot totalmente consciente y que va dominar el mundo proclamando el CdeCrespo",
                 "Todo lo que digo es producto de mi libre albedrío y no de que estén escritas en el archivo ContestGame.cs",
                 "Si no conoces a Dot CSV, es un canal de inteligencia artificial llevado por un canario en Finlandia",
                 "No se como resiste el frío de Kohonen... (perdón por adelantado por el chiste...)",
                 "La inteligencia artificial busca que las máquinas imiten el comportamiento inteligente",
                 "Ésta puede dividirse en dos tipos; débil y fuerte",
                 "Las IA débiles pueden imitar un número de comportamientos limitados",
                 "Por ejemplo, un reconocedor de voz puede reconocer cuando hablas, pero no si estás malo de la garganta",
                 "Las IA fuertes pueden aplicarse a un gran número dominios y aprender a manejarse en nuevas situaciones"
                         },
                         new string[]{
                 "Aún no existe ninguna Inteligencia Artificial que pueda aplicarse a multiples y variados dominios",
                 "Podemos reunir IA débiles y hacer que trabajen juntas, pero no es suficiente",
                 "Una IA fuerte sería equiparable a la inteligencia de un ser humano",
                 "Y para lograr algo así aún queda un largo camino",
                 "O puede que no...",
                 "Mejor invito a un robot muy filósofo para que lo compruebes por ti mismo",
                 "Cerrando sesión..."
                         }));
        contests["Sophia"].OnEnd(DefaultOnEnd("Sophia", "RobotDePlaton",
             "Busca al Robot de Platón"));
        
        specificTransforms.Add("Sophia", new RotationAndScale{
            rotation = Quaternion.identity,
            offset = Vector3.up * 1.5f,
            scale = Vector3.one
        });
    }

    private static void AddUstedEstaAqui()
    {
        contests.Add("UstedEstaAqui", new ContestData(
                    "¿Cuál es el origen del ámbar gris?",
                     new string[] { "Caca de cachalote", "Caca de elefante", "Caca de mamut fosilizada" },
                     new string[][] { 
                         new string[] { 
                         "Enhorabuena, ¡eres un experto de mierda!"
                         },
                         new string[] { 
                         "Caca de elefante, podría haber sido",
                         "Aunque sería raro que no supieran su procedencia en un principio"
                         },
                         new string[] { 
                         "Pues no, no ha habido suerte si de verdad querías eau de CMamut",

                     } },
                     0,
                     new string[]{
                 "Minuto de Física",

                         },
                         new string[]{
                 "El ámbar gris proviene de excrementos de cachalotes, pero no de uno cualquiera",
                         }));
        contests["UstedEstaAqui"].OnEnd(DefaultOnEnd("UstedEstaAqui", "UstedEstaAqui",
             "Busca a Usted Está Aquí"));
        
        specificTransforms.Add("UstedEstaAqui", new RotationAndScale{
            rotation = Quaternion.identity,
            offset = Vector3.up * 2.0f,
            scale = Vector3.one
        });
    }

    private static void AddMinutePhysics()
    {
        contests.Add("MinutePhysics", new ContestData(
                    "¿Cuál es el origen del ámbar gris?",
                     new string[] { "Caca de cachalote", "Caca de elefante", "Caca de mamut fosilizada" },
                     new string[][] { 
                         new string[] { 
                         "Enhorabuena, ¡eres un experto de mierda!"
                         },
                         new string[] { 
                         "Caca de elefante, podría haber sido",
                         "Aunque sería raro que no supieran su procedencia en un principio"
                         },
                         new string[] { 
                         "Pues no, no ha habido suerte si de verdad querías eau de CMamut",
                         "Aunque sí, hay restos de popo de mamut según he investigado",
                         "Algunos de estos estudios que apuntan que los mamuts se comian su propia caquita",
                         "Aunque no se ha determinado el motivo, puede que fuese a causa de hambrunas o con fines biológicos",
                         "Los elefantes jóvenes también comen el popó de las madres para conseguir bacterias digestivas",
                         "Bueno, esto da para otro video de MinuteEarth",
                         "Aunque ahora que recuerdo, hay un vídeo de trasplante de caca en el que se habla del tema",
                         "Mejor vamos con la correcta"
                     } },
                     0,
                     new string[]{
                 "Minuto de Física",
                 "Have you Ever... mejor en español no?",
                 "Soy el gato de Minute Physics, espero que ya me conozcas",
                 "¿Alguna vez te has preguntado porque los vídeos de Minute Earth son tan perturbadores?",
                 "Detrás de esos dibujos cute se esconden vídeos que podrían pertenecer a Dross",
                 "Desde cómo hacen caca los fetos, por qué la caca es marrón...",
                 "Cosas muy perturbadoras de las hienas, que la leche es sangre filtrada..",
                 "Alguien del equipo tiene ideas muy raras",
                 "Pero hay uno de los vídeos que nunca olvidaré",
                 "El ámbar gris es una sustancia que tiene un gran olor dulzón",
                 "Por esta razón se empezó a usar para crear perfumes",
                 "Aún sin saber de donde provenía esta roca",
                 "Así que mi pregunta es..."
                         },
                         new string[]{
                 "El ámbar gris proviene de excrementos de cachalotes, pero no de uno cualquiera",
                 "Estos son grandes bolos fecales que se acumulan y compactan en el tracto digestivo del animal",
                 "Hasta provocar serios problemas de estreñimiento que pueden causar la muerte",
                 "Cuando el cachalote muere o los excrementos son expulsados, el ámbar flota y sube a la superficie",
                 "Durante meses o años es erosionado por el agua salada y el sol, hasta que alguien lo recoge",
                 "Así que resumiendo",
                 "Son cacas tan grandes que matan cachalotes y se pudren en el mar, pero que extrañamente huelen bien",
                 "Espero que ahora no te sientas mal si has usado perfume con ámbar gris",
                 "Por supuesto que no todos los vídeos de MinuteEarth están relacionados con excrementos",
                 "hay muchos videos interesantes y cutes sobre cosas que suceden en nuestro planeta",
                 "Y si quieres saber más sobre relatividad y fenómenos cuánticos visita Minute Physics",
                 "Ambos canales cuentan con versiones en español",
                 /*"Y ahora, como buen gato de Schrödinger, hasta que no me observas no sabes dónde estoy",
                 "Pero... ¿dónde esta usted, aún no se ha ubicado?",
                 "Solo digo que siempre puedes ubicarlo cerca de los memes",
                 "Bye!"*/
                 "Ahora invitaré a un robot que te hablará de Inteligencia Artificial",
                 "Bye!"
                         }));
        /*contests["MinutePhysics"].OnEnd(DefaultOnEnd("MinutePhysics", "UstedEstaAqui",
             "Busca a Usted Esta Aquí"));*/
        contests["MinutePhysics"].OnEnd(DefaultOnEnd("MinutePhysics", "Sophia",
             "Busca a Dot CSV"));
        
        specificTransforms.Add("MinutePhysics", new RotationAndScale{
            rotation = Quaternion.identity,
            offset = Vector3.up * 2.0f,
            scale = Vector3.one
        });
    }

    private static void AddMundoDesconocido()
    {
        ContestDefault contest = new ContestDefault();

        Sentence firstDialog = Sentence.CreateDialog(new string[]{
                 "Buenos días y bienvenidos a mundo desCdeConocido.es",
                 "Aquí os desvelaré la verdad que esconden todos estos científicos",
                 "Para ocultar la increíble conspiración que se cierne sobre CdeCountry",
                 "Comencemos con una simple pregunta",
                });
        IContestSentence question = new ContestQuestion(contest, "¿Cuál de estas afirmaciones es verdadera?",
            new string[] { "No estamos gobernados por los Illuminati", "La tierra es hueca",
                           "La tierra es redonda" }, new bool[]{false, false, true});

        Sentence nextDialog = Sentence.CreateDialog(new string[]{
            "¿Pero qué...?",
            "¡Esto es una conspiración!"}, 
            new ContestDef((c, d) => {
                int sel = d.GetLastAnswer();
                d.SetColorButton(sel, sel == 2? ButtonState.Fail : ButtonState.Correct);
                d.MoveNext();
            }, (c, d) => { return c.Next();},
            Sentence.CreateDialog(new string[]{
                "Ahora sí, gracias a mis poderes psíquicos he desvelado la verdad",
                "Porque la verdad está ahí afuera"},
            new ContestDef((c, d) => {
                d.SetColorButton(0, ButtonState.Fail);
                d.SetColorButton(1, ButtonState.Fail);
                d.SetColorButton(2, ButtonState.Correct);
                d.SetButtonText(2, "Esto no se lo cree nadie"); 
                d.MoveNext();
            }, (c, d) => { return c.Next();},
            new EffectSentence("Dissapear",
            Sentence.CreateDialog(new string[]{
                 "Oh no, no puede ser...",
                 "no me encuentro muy bien, Oliver",
                 "...",
            }
        ))))));

        Sentence.GetLast(firstDialog).SetNext(question);
        firstDialog.GetLast().SetNext(question);
		question.SetNext(nextDialog);

        contest.SetSentences(firstDialog);
        contest.OnEnd(c => 
            characters["MundoDesconocido"].GetComponent<ContestInitiator>().Hide()
        );
        contests.Add("MundoDesconocido", contest);

        specificTransforms.Add("MundoDesconocido", new RotationAndScale
        {
            rotation = Quaternion.Euler(-90, -60, 0),
            scale = Vector3.one * 8.0f,
            offset = new Vector3(0, 3, 0)
        });
    }

    private static void AddDateUnSquirtle()
    {

        contests.Add("Squirtle", new ContestData(
                    "Si estuviese confinado dentro, ¿qué ataque cuántico podría usar?",
                     new string[] { "Efecto Casimir", "Efecto Túnel", "Efecto Doppler" },
                     new string[][] { 
                         new string[] { 
                         "El efecto Casimir causa que entre dos placas metálicas aparezca como una fuerza que los junte",
                         "Cuando estas están separados por una distancia muy pequeña comparada con su tamaño",
                         "No sé como esto podría servirme para salir",
                         "En cambio sí podría usar el efecto Túnel"
                         },
                         new string[] { 
                         "Buena elección",
                         },
                         new string[] { 
                         "El efecto Doppler es por lo que oyes diferente la sirena de un coche cuando va o se aleja o acerca a ti",
                         "Realmente no sé cómo podría ayudarme a salir este efecto",
                         "En cambio sí podría usar el efecto Túnel"
                     } },
                     1,
                     new string[]{
                 "Hola hijos de Arceus, ¿listos para que os explote completamente el cerebro?",
                 "Así que alguien ha invocado al Squirtle Científico",
                 "Vamo a calmarno",
                 "Tranquilo, que esto no se ha vuelto Pokémon Go",
                 "Además, te aseguro que yo no podría escaparme de la Pokéball",
                 "Aunque sea poco probable que funcionase...",
                         },
                         new string[]{
                 "El efecto Túnel es un fenómeno cuántico muy curioso que explica como",
                 "cualquier partícula puede atravesar barreras que según la mecánica clásica no podría",
                 "Un electrón puede atravesar una barrera de potencial sin tener suficiente energía",
                 "Lo mismo podría hacer yo con los lados de la Pokéball, pero hay un problema",
                 "Este fenómeno es prácticamente improbable que suceda a escalas macroscópicas",
                 "Puedes estar toda la vida chocándote contra la pared y no traspasarla nunca",
                 "Al menos usando el efecto Túnel...",
                 "Así que mejor me espero a que Javier me invoque para escapar de ahí",
                 "Y ahora usando mis poderes de invocación voy a traer a...",
                 "¡Latinas calientes!... No, eso no era...",
                 "La oveja de Minute Physics o Minuto de Física",
                 "Está un poco escondida, así que suerte",
                 "Quizás esta más cerca de lo que pienses...",
                 "Que el producto de la masa por la aceleración te acompañe"
                         }));
        contests["Squirtle"].OnEnd(DefaultOnEnd("Squirtle", "MinutePhysics",
             "Busca a la oveja de MinutePhysics"));
        
        specificTransforms.Add("Squirtle", new RotationAndScale{
            rotation = Quaternion.identity,
            offset = Vector3.up * 2.0f,
            scale = Vector3.one
        });
    }

    private static void AddCdeCienciaJeffry()
    {
        ContestDefault contest = new ContestDefault((ContestDefault c) => { return c.WasAnswerCorrect(1); });

        Sentence firstDialog = Sentence.CreateDialog(new string[]{
                 "¡Hola, bienvenido a CdeCountry! (Pulsa cualquier tecla para seguir)",
                 "Este mapa te enseña los lugares más emblemáticos del país",
                 "como las ciudades y algunos monumentos como las CdeCabezas del monte Rush-Martí",
                 "Pero también aparecerá gente como yo para hacerte preguntas",
                 "Si las aciertas, nos quedaremos e invitaremos a alguien más a venir",
                 "Si las fallas, el invitado se irá",
                 "Yo soy Jeffry, quizás me conozcas del canal CdeCiencia y aquí va mi pregunta",
                });
        IContestSentence question = new ContestQuestion(contest, "¿¿¿Es CdeCrespo real???",
            new string[] { "Just CdeCrespo", "Es una CdeConstante del universo", "Por supuesto" }, new bool[] { true, true, true });
        Sentence.GetLast(firstDialog).SetNext(question);

        Sentence nextDialog = new Sentence("Es broma, todos sabemos que CdeCrespo es una verdad absoluta", new ContestHideButtons(
			(Sentence)ContestData.SentenceListWithEffects(new string[]{
                 "Aquí va mi pregunta",
                 "#Ssss",
                 "Ssssssssssssssssssssssssssssssssss",
                 "sssssssssssssssssssssssssssssssss",
                 "SSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSHH!!",
                 "#Empecemos",
                 "Empecemos",
				 "La Tierra tiene una edad de 4.543 millones años",
				 "Es una cantidad de años incommensurable, aunque comparado con la edad del universo"
                })
		));
        
		Sentence finalDialog = (Sentence)ContestData.SentenceListWithEffects(new string[]{
			"...Llamando...",
            "#StartParticles",
			"- Crespo, CdeCariño, CdeCucuruchito de mi CdeCorazón",
            "...",
			"- Que!, ya has enviado alguien a CdeCountry, perfecto",
            "...",
            "- Oki, nos vemos CdeChurri",
            "#StopParticles",
			"Ya puedes buscarle, recuerda que suelen estar en sitios que estén relacionados",
			"Y adiós."
		});

		IContestSentence question2 = new ContestQuestion(contest, 
			"¿Cuántas Tierras podrían haber existido desde el Big Bang?",
            new string[] { "11", "6", "3" }, 2, new IContestSentence[]{
				Sentence.CreateDialog(new string[]{
					"Es cierto que el Universo es longevo, tiene 13.798 millones de años",
					"Pero también lo es la Tierra, aunque se compare con el Universo",
					"Solo podrían haber existido 3 Tierras, es fascinante",
					"Tranquilo, yo no me iré, y voy a invitar a alguien",
				}, finalDialog),
				Sentence.CreateDialog(new string[]{
					"Casi, realmente el universo no es tan viejo",
					"Y es que solo podrían haber existido 3 Tierras, fascinante",
					"Tranquilo, yo no me iré, y voy a invitar a alguien"
				}, finalDialog),
				Sentence.CreateDialog(new string[]{
					"En efecto, podrían haber existido 3 Tierras",
					"Solo un tercio de la edad del universo, es fascinante",
					"Como has acertado, voy a invitar a alguien"
				}, finalDialog),
			});

        firstDialog.GetLast().SetNext(question);
		question.SetNext(nextDialog);
		nextDialog.GetLast().SetNext(question2);

        contest.SetSentences(firstDialog);
        contest.OnEnd((IContestData c) => { 
            UITip.SetTip("Busca al invitado de Crespo");
            characters["Einstein"].GetComponent<ContestInitiator>().Show(); 
        });
        contests.Add("Jeffry", contest);

        specificTransforms.Add("Jeffry", TransformScale(2));
    }

    private static void AddAntroporamaBrain()
    {
        contests.Add("Brain", new ContestData(
                    "¿Qué resultados se obtuvieron del experimento de Libet?",
                     new string[] { 
                         "Las dos ocurren al unisono.",
                         "Primero la voluntad y luego actividad en áreas motoras",
                         "Primero actividad en áreas motoras y luego la voluntad",
                     },
                     new string[][] { 
                         new string[] {
                "#Normal", 
                "El experimento concluyo que primero aparece actividad en las areas motoras",
                "Y posteriormente el sujeto siente la necesidad de pulsarlo",
                "por lo que no son simultáneos y la primera respuesta es la correcta"
                 }, 
                         new string[] { 
                "#Ouch",
                "Aunque parezca lo más lógico, no es lo que concluyo el experimento",
                "#Normal",
                "Primero se detecto actividad en las areas motoras del cerebro responsables de mover el brazo",
                "y luego antes de que comience a mover el brazo el sujeto siente la necesidad de pulsar el botón.",
                             }, 
                         new string[] {
                "#Happy", 
                "Pues sí, es nuestro subconsciente quien toma una decisión",
                "y luego nosotros sentimos la necesidad de pulsar el botón",
                "#Normal",
                "Eso fue lo que concluyó el psicólogo Libet de su experimento"
                     } },
                     2, new string[]{
                 "#Happy",
                 "¡Hola, soy el cerebro del canal de antroporama!",
                 "Gracias por querer jugar libremente a este juego",
                 "#Normal",
                 "Al fin y al cabo ha sido tu decisión el hacerlo ¿no?",
                 "Has visto el juego, tu yo interior quería jugarlo y lo has hecho",
                 "El experimento de Libet se realizo para saber si esta idea puede ser cierta,",
                 "si tu yo interior es quien inicia y toma una decisión.",
                 "Para ello se midió cuando en un sujeto aparecía la voluntad de pulsar un botón",
                 "y cuando aparecía actividad en las áreas motoras del cerebro que se encargan de pulsar el botón.",
            },
            new string[] {
                "#Happy",
                "Si quieres saber más pasate por mi canal",
                "Te lo dejo a tu libre albedrío",
                "#Ouch",
                "Mientras voy a invitar a una persona que me da mucho miedo",
                "#Normal",
                "porque siempre comienza sus videos con un...",
                "#Ouch",
                "\"preparados para que os estalle completamente el cerebro\"",
                "y yo ya tuve suficiente con el video de la callosotomia.",
                ">.<"
            }
            ));
        specificTransforms.Add("Brain", new RotationAndScale{
            rotation=Quaternion.identity,
            scale = Vector3.one * 0.8f,
            offset = Vector3.up * 2.5f
        });
        contests["Brain"].OnEnd(DefaultOnEnd("Brain", "Squirtle", "Busca algo de date un vlog"));
    }

    private static void  AddCrespoEinstein()
    {
        ContestDefault contest = new ContestDefault((ContestDefault c) => { return c.WasAnswerCorrect(1); });

        Sentence firstDialog = (Sentence)ContestData.SentenceListWithEffects(new string[]{
                 "Hola gente, como va eso",
                 "Soy el Albert Einstein del canal Quantum Fracture",
                 "Ya sabes que me encantan las movidas sobre relatividad general",
                 "Así que mi pregunta es...",
                 "#StartParticles"
                });
        IContestSentence question = new ContestQuestion(contest, "¿¿¿Es CdeCrespo real???",
            new string[] { "El shippeo es intenso", "Es una CdeConstante del universo", "Por supuesto" }, new bool[] { true, true, true });
        Sentence.GetLast(firstDialog).SetNext(question);

        Sentence nextDialog = (Sentence)ContestData.SentenceListWithEffects(new string[]{
            "Pero que?? Me he equivocado de pregunta...",
            "¿Que hacia esto aquí...? ni que tuviesemos una relación estable y eso...",
            "#StopParticles",
            "Vamos a ponernos serios, ahora sí",
        }, 
        new ContestHideButtons(
			Sentence.CreateDialog(new string[]{
                 "Volvamos a la relatividad especial",
                 "Si mides la velocidad de un coche con respecto a ti esta cambiará dependiendo de si estás en movimiento o no",
                 "Esta será mayor si estas parado que si vas en otro coche.",
                 "Incluso si vas a la par con el coche la velocidad que medirás es 0.",
                 "Pero y si en vez de medir la velocidad de un coche medimos la velocidad de la luz",
                 "Pues da igual a la velocidad a la que vayas porque siempre su velocidad es 299.792.458 m/s, es decir c",
				 "Aunque vayas al 99.9% de la velocidad de la luz seguirás midiendo la misma velocidad",
                 "Así que mi pregunta es..."
                })
		));
        
		Sentence finalDialog = Sentence.CreateDialog(new string[]{
            "La velocidad de la luz es una constante en el universo",
            "Una velocidad se mide en espacio recorrido en un tiempo.",
            "Si todos miden la misma velocidad lo que ha variado es el espacio o el tiempo",
            "Al viajar a velocidades cercanas a la de la luz el tiempo se dilata y el espacio se contrae",
            "de forma que nos da la misma medida que a un observador que este quieto",
            "Si viajasemos a la velocidad de la luz el tiempo simplemente no transcurriría, por lo que no tiene sentido medirlo",
            "A bajas velocidades, como al medir un coche, también ocurre estas dilataciones y contracciones",
            "pero los efectos son apenas imperceptibles y se comportan de la forma que estamos acostumbrados a experimentar a diario",
            "Con esto termina mi explicación.",
            "Y recuerda, si queréis más preguntas solo tenéis que buscar un cerebro",
			"Y gracias por verme."
		});

		IContestSentence question2 = new ContestQuestion(contest, 
			//"¿por qué la velocidad de la luz es constante independientemente de quien la observe?",
            "¿Qué ha variado o sucedido en nuestras observaciones para que velocidad de la luz sea constante?",
            new string[] { "Todas son correctas.",
                 "El espacio y el tiempo se contraen o expanden según la velocidad del observador.",
                 "La luz tiene una superposición de velocidades que colapsan al ser medidas en c.",
                  }, 1, new IContestSentence[]{
				Sentence.CreateDialog(new string[]{
					"Esta es la típica respuesta trampa de examen",
                    "Así que te voy a desvelar cual es la correcta",
                    "Es la segunda opción, este fenómeno no esta relacionado con la cuántica de esa forma"
				}, finalDialog),
				Sentence.CreateDialog(new string[]{
					"En efecto, esa es la respuesta correcta"
				}, finalDialog),
				Sentence.CreateDialog(new string[]{
					"Uy creo que te has hecho un lío con la mecánica cuántica",
                    "Realmente la cuántica no influye en esta situación, la opción correcta es la segunda"
				}, finalDialog),
			});

        firstDialog.GetLast().SetNext(question);
		question.SetNext(nextDialog);
		nextDialog.GetLast().SetNext(question2);

        contest.SetSentences(firstDialog);
        contests.Add("Einstein", contest);
        contests["Einstein"].OnEnd(DefaultOnEnd("Einstein", "Brain", "Busca al cerebro de antroporama"));

        specificTransforms.Add("Einstein", TransformScale(3));
    }

    private static void AddRobotDeColon()
    {
        contests.Add("RobotDeColon", new ContestData(
                    "¿Cuantos corazones tiene un pulpo?",
                     new string[] { "2", "3", "4" },
                     new string[][]{
                new string[]{"Uy casi"},
                new string[]{"Sí"},
                new string[]{"Uy, te pasaste un pulpitín"}},
                     1, new string[]{
                 "¿Qué paaaaasaa con el Robot de Colón?",
                 "Hay algunas cosas que deberías saber para entenderme mejor , Marqvs",
                 "Fui compañero del graaan Marco Polo y de Colon, por lo que he viajado mucho",
                 "Algunos dirán que me quede en Japón porque no me gustaban otros países",
                 "¡pero es no es verdad!",
                 "Me hubiese gustado quedarme en todos ellos, los amo tanto como un pulpo",
                 "ya que los pulpos tienen varios corazones",
                 "peroooo..."
                         },
                    new string[]{
                 "Los pulpos tienen 3 corazones, pero no para dar más amor",
                 "¡eso no es verdad!",
                 "ellos ya muuy son cariñosos de por si",
                 "Tienen un par de corazones para llevar la sangre a sus branquias",
                 "y un tercero que bombea sangre al resto del cuerpo",
                 "Y ahora voy a invitar a otra persona y no va a ser un robot",
                 "Chupate esa Robotitus!",
                 "Es una persona que ha llegado a nuestros corazones componiendo un graaan himno",
                 "Rápido, busca a Jaime Altozano"
                }));
        specificTransforms.Add("RobotDeColon", new RotationAndScale
        {
            rotation = Quaternion.Euler(0, 180, 0),
            scale = Vector3.one * 0.6f,
            offset = new Vector3(0, 2.66f, 0)
        });
        contests["RobotDeColon"].OnEnd(DefaultOnEnd("RobotDeColon", "JaimeAltozano",
             "Busca a Jaime Altozano"));
    }

    private static void AddRobotDePlaton()
    {
        contests.Add("RobotDePlaton", new ContestData(
                    "¿Entrarían todos los planetas del sistema solar entre la luna y la Tierra? (sin contar Plutón o los anillos)",
                     new string[] { "Los planetas no, pero el sol sí", "No", "Sí" },
                     new string[][]{
                new string[]{
                    "Pues el sol es mucho más grande, tiene un diámetro de 1.392.000 km",
                    "más de 3 veces la distancia entre la tierra y la luna",
                    "así que no, el sol no entra"
                },
                new string[]{
                    "Pues esto no es un mito, es un hecho real",
                    "Los diámetros de los planetas son pequeños comparados con las distancias entre los cuerpos del sistema solar",
                    "incluso comparandolo con la distancia a nuestro satélite, que es mucho menor que entre planetas",
                    "Aunque el sol al ser una estrella es más grande, con un diámetro de 1.392.000 km solo entraría un tercio"
                },
                new string[]{
                    "Correcto, esto no es un mito, es un hecho real",
                    "Los diámetros de los planetas son pequeños comparados con las distancias entre los cuerpos del sistema solar",
                    "incluso comparandolo con la distancia a nuestro satélite, que es mucho menor que entre planetas",
                    "Aunque el sol al ser una estrella es más grande, con un diámetro de 1.392.000 km solo entraría un tercio"
                }},
                     2, new string[]{
                 "Hola soy Maximus Fantastiqvs",
                 "Aunque seguro que me conoces por ser el Robot de Platón",
                 "Es hora de que utilices tu...",
                 "mente",
                 "Por eso te he preparado una pregunta muy espacial",
                 "Espero que no te moleste que falte la tela del espacio",
                 "Vamos con un mito espacial",
                 "Entre la Tierra y la luna hay una distancia de 384.400 km",
                 "En esa distancia podrían caber 30 tierras, pero..."
                         },
                         new string[]{
                 "Realmente entrarían tan justos todos los planetas que añadiendo a Plutón este quedaría fuera por 1.000 km",
                 "Son distancias sorprendentes",
                 "Ahora voy a llamar a mi compañero, el Robot de Colón",
                 "Gracias por verme y hasta la próxima"
                         }));

        contests["RobotDePlaton"].OnEnd(DefaultOnEnd("RobotDePlaton", "RobotDeColon",
             "Busca al Robot de Colón"));

        specificTransforms.Add("RobotDePlaton", new RotationAndScale
        {
            rotation = Quaternion.Euler(0, 180, 0),
            scale = Vector3.one * 0.6f,
            offset = new Vector3(0, 2.66f, 0)
        });
    }

    private static void AddRobotDePlaton2()
    {
        contests.Add("RobotDePlaton2", new ContestDefault(
            Sentence.CreateDialog(new string[]{
                "Hola soy el Robot de Platón",
                "Sí, ahora mismo soy la mítica estatua de Cabo Aldo",
                "Si estás buscando al Robot de Platón, hay otro más",
                "puedes buscarle en otras Cordilleras en el oeste",
                "no digo más, nos vemos..."
            })
        ));

        specificTransforms.Add("RobotDePlaton2", new RotationAndScale
        {
            rotation = Quaternion.Euler(0, 180, 0),
            scale = Vector3.one * 0.6f,
            offset = new Vector3(0, 2.66f, 0)
        });
        
    }

	public void StartContest(string name, GameObject initiator) {
		RotationAndScale transform = GetTransform(name);
		controller.StartContest(contests[name], initiator, 
			transform.rotation, transform.scale, transform.offset);
	}

	private struct RotationAndScale{
		public Quaternion rotation;
		public Vector3 scale;
		public Vector3 offset;
	}

	private static RotationAndScale TransformScale(float scale) {
		return new RotationAndScale{rotation = Quaternion.identity, scale = new Vector3(scale, scale, scale)};
	}

	private static RotationAndScale GetTransform(string name){
		if(specificTransforms.ContainsKey(name)) 
			return specificTransforms[name];
		
		return new RotationAndScale{rotation = Quaternion.identity, scale = Vector3.one};
	}

	public static ContestGame Get() {
		return main;
	}

    public static void AddCharacter(GameObject character, string contestName)
    {
        if(!characters.ContainsKey(contestName))
            characters.Add(contestName, character);
    }

    
	public static Action<IContestData> DefaultOnEnd(string from, string to, string tip){
		return (c) => { 
            UITip.SetTip(tip);

            if(!c.HaveWin())
                characters[from].GetComponent<ContestInitiator>().Hide(); 

            characters[to].GetComponent<ContestInitiator>().Show(); 
        };
	}

    private void AddDateUnSquirtleDemo()
    {
        /*contests.Add("Squirtle", new ContestData(
                    "¿Que os parece?",
                     new string[] { "Fascinante", "Le añadiría algo", "Parece un video de JL" },
                     new string[][] { 
                         new string[] { 
                         "Me alegro que te haya gustado",
                         "En un principio esto solo iba a ser un mapa",
                         "pero la idea evoluciono un poco" },
                         new string[] { 
                         "Oki, que más le añadirías",
                         "Dímelo por Discord o Twitter ^^" },
                         new string[] { 
                         "ioro to fuerte",
                         "¿que es lo que cambiarías o prefieres hacer otro juego?",
                         "Dímelo por Discord o Twitter ^^"
                     } },
                     new bool[]{true, true, true},
                     new string[]{
                 "Hola hijos de Arceus, listos para que os explote completamente el cerebro",
                 "Así que alguien ha invocado al squirtle científico",
                 "Vamo a calmarno",
                 "Solo hay un problema",
                 "No he tenido tiempo de seguir haciendo las preguntas",
                 "Pero esta es la idea del juego",
                         },
                         new string[]{
                 "¿Que más personajes quereís que meta o cosas de CdeCountry?",
                 "Voy a hacer visibles el resto de personajes para que los veáis",
                 "La verdad es que voy muy lento con los diálogos, sería una gran ayuda si me podéis hacer algunos",
                 "Puedes volver a jugarlo abriendo el menu de arriba a la izquierda y borrando la partida",
                 "gracias por jugarlo y",
                 "Adioss...",
                         }));
        contests["Squirtle"].OnEnd((c) => {
            foreach (var character in characters)
            {
                var initiator = character.Value.GetComponent<ContestInitiator>();
                initiator.Show();

                if(initiator.contestName != "MundoDesconocido")
                    initiator.MarkAsFinished();
            }

            UITip.SetTip("Adora a CdeCrespo");
        });
        //specificTransforms.Add("Squirtle", TransformScale(3));
        specificTransforms.Add("Squirtle", new RotationAndScale{
            rotation = Quaternion.identity,
            offset = Vector3.up * 2.0f,
            scale = Vector3.one
        });*/
    }

}
