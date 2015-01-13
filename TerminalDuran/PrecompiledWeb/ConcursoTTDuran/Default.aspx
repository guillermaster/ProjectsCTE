<%@ page title="" language="C#" masterpagefile="~/Site.master" autoeventwireup="true" inherits="_Default, App_Web_default.aspx.cdcab7d2" theme="SkinFile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <script type="text/javascript">
        function nextStep() {
            $("#intro").fadeOut("slow");
            setTimeout(function () { $("#bases").fadeIn("slow"); }, 600);
        }
        function nextStepOpiniones() {
            $("#bases").fadeOut("slow");
            setTimeout(function () { $("#opiniones").fadeIn("slow"); }, 600);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <div id="intro">
        <h2>Concurso</h2>
        <h1>Un nombre para el nuevo Terminal Terrestre del Cantón Durán</h1>
        <div style="text-align: center">
            <img src="images/ttduranMain.jpg" alt="Terminal Terrestre" style="margin: 0 0 0 -20px" />
        </div>
        <p>Con el objetivo de denominar al nuevo Terminal Terrestre de Durán, la Comisión de Tránsito del Ecuador, convoca a todos los habitantes de la provincia del Guayas a participar en el concurso <strong>“UN NOMBRE PARA EL NUEVO TERMINAL TERRESTRE DEL CANTON DURAN”</strong>.</p>
        <p>Este concurso se realiza en vista que estamos próximos a inaugurar esta obra emblemática para este cantón cercano a Samborondon y Guayaquil en el que habitan 235.769 ecuatorianos.</p>
        <p>Esta es la segunda vez en la historia del país que el Gobierno promueve un concurso en el que se promueve la participación de la ciudadanía.</p>
        <p>El Terminal ubicado en el Kilómetro 3,5 de la VIA DURAN – BOLICHE (junto al Durán OUTLET) tiene un área de 17.708,36 m2. Cuenta con paradero de buses públicos, 65 estacionamientos de vehículos particulares y taxis, 12 andenes de llegada y patio de maniobra, 12 andenes de salida y patio de maniobra, un bloque principal de áreas de administración y 24 locales de cooperativas de transporte, área de servicios generales, área de la CTE, área de la policía nacional, 1737m2 de áreas verdes.</p>
        <p>Esperamos que el concurso despierte el interés de toda la población por lo que:</p>
        <p>Invitamos a todos los Guayasenses a participar  en esta nominación desde el ______________________________ del 2011.</p>
        <div onclick="javascript:nextStep();" style="cursor: pointer" class="nextButton"></div>
    </div>
    <div id="bases" style="display: none;">
        <h2>Bases del Concurso</h2>
        <div style="text-align: center">
            <img src="images/ttduranS1.jpg" alt="Terminal Terrestre" style="margin: 0 0 0 -20px" />
        </div>        
        <ul>
            <li>Nombre del concursante</li>
            <li>Cédula de identidad</li>
            <li>E-mail</li>
            <li>Número telefónico convencional o celular  (en caso de tenerlo).</li>
            <li>Nombre sugerido para el T. T. de Durán.</li>
            <li>Justificación histórica o sustentación del nombre propuesto.</li>
        </ul>
        <p>A partir del ________________________ se receptará la votación del público a través de la página web www.cte.gob.ec o enviando en sobre cerrado al Departamento de Relaciones Públicas de la CTE situado en el primer piso de la Comisión de Tránsito del Ecuador (Chile 1710 y Cuenca).</p>
        <p>Por razones éticas y sociales se hace énfasis en evitar los nombres de personajes vivos y de partidos políticos.</p>
        <p>Sólo se tomarán en cuenta los nombres de personalidades fallecidas, fechas, eventos o acontecimientos históricos.</p>
        <h3>PLAZO</h3>
        <p>El plazo para la presentación de sugerencias del público es desde el __________________ hasta el ___________________ del año en curso.</p>
        <p>Luego de esta primera fase  empezará la segunda que es el análisis y pre selección de las propuestas recibidas, a cargo de un jurado calificador integrado por:</p>
        <strong>(JURADO CALIFICADOR SUGERIDO)</strong>
        <ul>
            <li>Lcdo. Jimmy Jairalla, Prefecto Provincial del Guayas</li>
            <li>Ing. Roberto Cuero, Gobernador de la Provincia del Guayas</li>
            <li>Dra. Raquel Mancero Castillo (catedrática jubilada del colegio nocturno “Eloy Alfaro de Durán).</li>
            <li>Jenny Estrada Ruíz – Historiadora.</li>
        </ul>
        <p>Una vez seleccionados los nombres, a partir del ______________y hasta el _______________ de diciembre se receptará la votación del público a través de la página web www.cte.gob.ec o enviando en sobre cerrado al Departamento de Relaciones públicas de la CTE situado en el primer piso de la Comisión de Tránsito del Ecuador (Chile 1710 y Cuenca).</p>
        <p>En esta etapa se deberá llenar los datos detallados a continuación:</p>
        <ul>
            <li>Nombre del concursante</li>
            <li>Cédula de identidad</li>
            <li>E-mail</li>
            <li>Número telefónico convencional o celular (en caso de tenerlo).</li>
            <li>Voto</li>
        </ul>
        <div onclick="javascript:nextStepOpiniones();" style="cursor: pointer" class="nextButton"></div>
    </div>
    <div id="opiniones" style="display: none">
        <h2>Opiniones del Público</h2>
        <div style="text-align: center">
            <img src="images/ttduranS2.jpg" alt="Terminal Terrestre" style="margin: 0 0 0 -20px" />
        </div>    
        <div style="width: 100%; margin: 20px 0; float:left">
            <div style="width: 200px; float: left">
                <img src="images/fotoPersSug1.png" alt="" />
            </div>
            <div style="width: 500px; float: left; font-size:x-large; font-style: italic;">
                "Me parece que este bien el Terminal  ya que en el centro comercial no biene mucha gente  y tal vez con el Terminal pueda venir mas personas". 
            </div>
        </div>
        <div style="width: 100%; margin: 20px 0; float: left">
            <div style="width: 500px; float: left; font-size:x-large; font-style: italic">
                "Pienso que va a ser de gran beneficio para nosotros los  que  tenemos negocio aquí porque así  va haber  mas afluencia de personas. Las personas ya no van a estar en la calle cogiendo carros, sino que van a poder ir tranquilamente a la terminal".
            </div>
            <div style="width: 200px; float: left">
                <img src="images/fotoPersSug2.png" alt="" />
            </div>            
        </div>
        <div style="width: 100%; float: left">
            <asp:LinkButton ID="btnNext" runat="server" CausesValidation="false" ClientIDMode="Static" 
             PostBackUrl="~/Voto.aspx" SkinID="BtnNext"></asp:LinkButton>
        </div>
    </div>
</asp:Content>

