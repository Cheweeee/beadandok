#include "MyApp.h"
#include "GLUtils.hpp"
#include <time.h>
#include <math.h>
#include <iostream>

CMyApp::CMyApp(void)
{
	m_vaoID = 0;
	m_vboID = 0;
	m_programID = 0;
}


CMyApp::~CMyApp(void)
{
}

bool CMyApp::Init()
{
	// törlési szín legyen kékes
	glClearColor(0.125f, 0.25f, 0.5f, 1.0f);

	//glEnable(GL_CULL_FACE); // kapcsoljuk be a hatrafele nezo lapok eldobasat
	glEnable(GL_DEPTH_TEST); // mélységi teszt bekapcsolása (takarás)
	glCullFace(GL_BACK); // GL_BACK: a kamerától "elfelé" nézõ lapok, GL_FRONT: a kamera felé nézõ lapok

	glPolygonMode(GL_BACK, GL_LINE);

	srand(time(NULL));
	
	int x = rand() % 10 - 5;
	int y = rand() % 10 - 5;
	int z = rand() % 10 - 5;
	for (int i = 0; i < 6; ++i) {
		do {
			x = rand() % 10 - 5;
			y = rand() % 10 - 5;
			z = rand() % 10 - 5;
		} while (pow(x, 3) + pow(y, 4) - x * y + x * pow(z, 3) >= 10);
	
		point[i] = {x,y,z} ;
	}
	
	//std::cout << x<<y<<z << std::endl;

	//
	// geometria letrehozasa
	//

	Vertex vert[] =
	{ 


		// elõlap
		//          x,  y, z             R, G, B
		{glm::vec3(0, 0, 1), glm::vec3((rand() % 100) / 100.0f, (rand() % 100) / 100.0f, (rand() % 100) / 100.0f)},
		{glm::vec3( 1, 0, 1), glm::vec3((rand() % 100) / 100.0f, (rand() % 100) / 100.0f, (rand() % 100) / 100.0f)},
		{glm::vec3(0, 1,  1), glm::vec3((rand() % 100) / 100.0f, (rand() % 100) / 100.0f, (rand() % 100) / 100.0f)},
		{glm::vec3(0, 1,  1), glm::vec3((rand() % 100) / 100.0f, (rand() % 100) / 100.0f, (rand() % 100) / 100.0f)},
		{glm::vec3(1, 0, 1), glm::vec3((rand() % 100) / 100.0f, (rand() % 100) / 100.0f, (rand() % 100) / 100.0f)},
		{glm::vec3( 1, 1,  1), glm::vec3((rand() % 100) / 100.0f, (rand() % 100) / 100.0f, (rand() % 100) / 100.0f)},

		//jobb oldal
		{glm::vec3(1, 1, 1), glm::vec3((rand() % 100) / 100.0f, (rand() % 100) / 100.0f, (rand() % 100) / 100.0f)},
		{glm::vec3(1, 0, 1), glm::vec3((rand() % 100) / 100.0f, (rand() % 100) / 100.0f, (rand() % 100) / 100.0f)},
		{glm::vec3(1, 0,  0), glm::vec3((rand() % 100) / 100.0f, (rand() % 100) / 100.0f, (rand() % 100) / 100.0f)},
		{glm::vec3(1, 0,  0), glm::vec3((rand() % 100) / 100.0f, (rand() % 100) / 100.0f, (rand() % 100) / 100.0f)},
		{glm::vec3(1, 1, 0), glm::vec3((rand() % 100) / 100.0f, (rand() % 100) / 100.0f, (rand() % 100) / 100.0f)},
		{glm::vec3(1, 1,  1), glm::vec3((rand() % 100) / 100.0f, (rand() % 100) / 100.0f, (rand() % 100) / 100.0f)},

		//hátlap
		{glm::vec3(1, 0, 0), glm::vec3((rand() % 100) / 100.0f, (rand() % 100) / 100.0f, (rand() % 100) / 100.0f)},
		{glm::vec3(0, 0, 0), glm::vec3((rand() % 100) / 100.0f, (rand() % 100) / 100.0f, (rand() % 100) / 100.0f)},
		{glm::vec3(1, 1,  0), glm::vec3((rand() % 100) / 100.0f, (rand() % 100) / 100.0f, (rand() % 100) / 100.0f)},
		{glm::vec3(1, 1,  0), glm::vec3((rand() % 100) / 100.0f, (rand() % 100) / 100.0f, (rand() % 100) / 100.0f)},
		{glm::vec3(0, 0, 0), glm::vec3((rand() % 100) / 100.0f, (rand() % 100) / 100.0f, (rand() % 100) / 100.0f)},
		{glm::vec3(0, 1,  0), glm::vec3((rand() % 100) / 100.0f, (rand() % 100) / 100.0f, (rand() % 100) / 100.0f)},

		//bal oldal
		{glm::vec3(0, 1, 0), glm::vec3((rand() % 100) / 100.0f, (rand() % 100) / 100.0f, (rand() % 100) / 100.0f)},
		{glm::vec3(0, 0, 0), glm::vec3((rand() % 100) / 100.0f, (rand() % 100) / 100.0f, (rand() % 100) / 100.0f)},
		{glm::vec3(0, 0,  1), glm::vec3((rand() % 100) / 100.0f, (rand() % 100) / 100.0f, (rand() % 100) / 100.0f)},
		{glm::vec3(0, 0,  1), glm::vec3((rand() % 100) / 100.0f, (rand() % 100) / 100.0f, (rand() % 100) / 100.0f)},
		{glm::vec3(0, 1, 1), glm::vec3((rand() % 100) / 100.0f, (rand() % 100) / 100.0f, (rand() % 100) / 100.0f)},
		{glm::vec3(0, 1,  0), glm::vec3((rand() % 100) / 100.0f, (rand() % 100) / 100.0f, (rand() % 100) / 100.0f)},

		//alap
		{glm::vec3(0, 0, 1), glm::vec3((rand() % 100) / 100.0f, (rand() % 100) / 100.0f, (rand() % 100) / 100.0f)},
		{glm::vec3(0, 0, 0), glm::vec3((rand() % 100) / 100.0f, (rand() % 100) / 100.0f, (rand() % 100) / 100.0f)},
		{glm::vec3(1, 0,  0), glm::vec3((rand() % 100) / 100.0f, (rand() % 100) / 100.0f, (rand() % 100) / 100.0f)},
		{glm::vec3(1, 0,  0), glm::vec3((rand() % 100) / 100.0f, (rand() % 100) / 100.0f, (rand() % 100) / 100.0f)},
		{glm::vec3(1, 0, 1), glm::vec3((rand() % 100) / 100.0f, (rand() % 100) / 100.0f, (rand() % 100) / 100.0f)},
		{glm::vec3(0, 0,  1), glm::vec3((rand() % 100) / 100.0f, (rand() % 100) / 100.0f, (rand() % 100) / 100.0f)},

		//tetõ
		{glm::vec3(0, 1, 0), glm::vec3((rand() % 100) / 100.0f, (rand() % 100) / 100.0f, (rand() % 100) / 100.0f)},
		{glm::vec3(0, 1, 1), glm::vec3((rand() % 100) / 100.0f, (rand() % 100) / 100.0f, (rand() % 100) / 100.0f)},
		{glm::vec3(1, 1,  1), glm::vec3((rand() % 100) / 100.0f, (rand() % 100) / 100.0f, (rand() % 100) / 100.0f)},
		{glm::vec3(1, 1,  1), glm::vec3((rand() % 100) / 100.0f, (rand() % 100) / 100.0f, (rand() % 100) / 100.0f)},
		{glm::vec3(1, 1, 0), glm::vec3((rand() % 100) / 100.0f, (rand() % 100) / 100.0f, (rand() % 100) / 100.0f)},
		{glm::vec3(0, 1,  0), glm::vec3((rand() % 100) / 100.0f, (rand() % 100) / 100.0f, (rand() % 100) / 100.0f)},

	};

	

	// 1 db VAO foglalasa
	glGenVertexArrays(1, &m_vaoID);
	// a frissen generált VAO beallitasa aktívnak
	glBindVertexArray(m_vaoID);
	
	// hozzunk létre egy új VBO erõforrás nevet
	glGenBuffers(1, &m_vboID); 
	glBindBuffer(GL_ARRAY_BUFFER, m_vboID); // tegyük "aktívvá" a létrehozott VBO-t
	// töltsük fel adatokkal az aktív VBO-t
	glBufferData( GL_ARRAY_BUFFER,	// az aktív VBO-ba töltsünk adatokat
				  sizeof(vert),		// ennyi bájt nagyságban
				  vert,	// errõl a rendszermemóriabeli címrõl olvasva
				  GL_STATIC_DRAW);	// úgy, hogy a VBO-nkba nem tervezünk ezután írni és minden kirajzoláskor felhasnzáljuk a benne lévõ adatokat
	

	// VAO-ban jegyezzük fel, hogy a VBO-ban az elsõ 3 float sizeof(Vertex)-enként lesz az elsõ attribútum (pozíció)
	glEnableVertexAttribArray(0); // ez lesz majd a pozíció
	glVertexAttribPointer(
		0,				// a VB-ben található adatok közül a 0. "indexû" attribútumait állítjuk be
		3,				// komponens szam
		GL_FLOAT,		// adatok tipusa
		GL_FALSE,		// normalizalt legyen-e
		sizeof(Vertex),	// stride (0=egymas utan)
		0				// a 0. indexû attribútum hol kezdõdik a sizeof(Vertex)-nyi területen belül
	); 

	// a második attribútumhoz pedig a VBO-ban sizeof(Vertex) ugrás után sizeof(glm::vec3)-nyit menve újabb 3 float adatot találunk (szín)
	glEnableVertexAttribArray(1); // ez lesz majd a szín
	glVertexAttribPointer(
		1,
		3, 
		GL_FLOAT,
		GL_FALSE,
		sizeof(Vertex),
		(void*)(sizeof(glm::vec3)) );

	glBindVertexArray(0); // feltöltüttük a VAO-t, kapcsoljuk le
	glBindBuffer(GL_ARRAY_BUFFER, 0); // feltöltöttük a VBO-t is, ezt is vegyük le

	//
	// shaderek betöltése
	//
	GLuint vs_ID = loadShader(GL_VERTEX_SHADER,		"myVert.vert");
	GLuint fs_ID = loadShader(GL_FRAGMENT_SHADER,	"myFrag.frag");

	// a shadereket tároló program létrehozása
	m_programID = glCreateProgram();

	// adjuk hozzá a programhoz a shadereket
	glAttachShader(m_programID, vs_ID);
	glAttachShader(m_programID, fs_ID);

	// VAO-beli attribútumok hozzárendelése a shader változókhoz
	// FONTOS: linkelés elõtt kell ezt megtenni!
	glBindAttribLocation(	m_programID,	// shader azonosítója, amibõl egy változóhoz szeretnénk hozzárendelést csinálni
							0,				// a VAO-beli azonosító index
							"vs_in_pos");	// a shader-beli változónév
	glBindAttribLocation( m_programID, 1, "vs_in_col");

	// illesszük össze a shadereket (kimenõ-bemenõ változók összerendelése stb.)
	glLinkProgram(m_programID);

	// linkeles ellenorzese
	GLint infoLogLength = 0, result = 0;

	glGetProgramiv(m_programID, GL_LINK_STATUS, &result);
	glGetProgramiv(m_programID, GL_INFO_LOG_LENGTH, &infoLogLength);
	if ( GL_FALSE == result )
	{
		std::vector<char> ProgramErrorMessage( infoLogLength );
		glGetProgramInfoLog(m_programID, infoLogLength, NULL, &ProgramErrorMessage[0]);
		fprintf(stdout, "%s\n", &ProgramErrorMessage[0]);
		
		char* aSzoveg = new char[ProgramErrorMessage.size()];
		memcpy( aSzoveg, &ProgramErrorMessage[0], ProgramErrorMessage.size());

		std::cout << "[app.Init()] Sáder Huba panasza: " << aSzoveg << std::endl;

		delete aSzoveg;
	}

	// mar nincs ezekre szukseg
	glDeleteShader( vs_ID );
	glDeleteShader( fs_ID );

	//
	// egyéb inicializálás
	//

	// vetítési mátrix létrehozása
	m_matProj = glm::perspective( 45.0f, 640/480.0f, 1.0f, 1000.0f );

	// shader-beli transzformációs mátrixok címének lekérdezése
	m_loc_world = glGetUniformLocation( m_programID, "world");
	m_loc_view  = glGetUniformLocation( m_programID, "view" );
	m_loc_proj  = glGetUniformLocation( m_programID, "proj" );

	return true;
}

void CMyApp::Clean()
{
	glDeleteBuffers(1, &m_vboID);
	glDeleteVertexArrays(1, &m_vaoID);

	glDeleteProgram( m_programID );
}

void CMyApp::Update()
{
	// nézeti transzformáció beállítása 
	float t = SDL_GetTicks() / 1000.0f;

	//szembõl (1-2 feladathoz)
	m_matView = glm::lookAt(glm::vec3(0, 10, 30),

	//fenti nézet (3. feladathoz)
	//m_matView = glm::lookAt(glm::vec3( 0,  40,  5),		// honnan nézzük a színteret

	//oldalsó nézet (4. feladathoz)
	//m_matView = glm::lookAt(glm::vec3(30, 20, 0),

	

							glm::vec3( 0,  0,  0),		// a színtér melyik pontját nézzük
							glm::vec3( 0,  1,  0));		// felfelé mutató irány a világban
}


void CMyApp::Render()
{
	// töröljük a frampuffert (GL_COLOR_BUFFER_BIT) és a mélységi Z puffert (GL_DEPTH_BUFFER_BIT)
	glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT);

	// shader bekapcsolasa
	glUseProgram( m_programID );
	glUniformMatrix4fv(m_loc_view, 1, GL_FALSE, &(m_matView[0][0]));
	glUniformMatrix4fv(m_loc_proj, 1, GL_FALSE, &(m_matProj[0][0]));
	glBindVertexArray(m_vaoID);
	SDL_Event event;
	while (SDL_PollEvent(&event))
	{
		if (event.type == SDL_KEYDOWN) {
			if (event.key.keysym.sym == SDLK_SPACE)
				needScale = !needScale;
		}
		
	}
	// shader parameterek beállítása
	/*

	GLM transzformációs mátrixokra példák:
		glm::rotate<float>( szög, glm::vec3(tengely_x, tengely_y, tengely_z) ) <- tengely_{xyz} körüli elforgatás
		glm::translate<float>( glm::vec3(eltol_x, eltol_y, eltol_z) ) <- eltolás
		glm::scale<float>( glm::vec3(s_x, s_y, s_z) ) <- léptékezés

	*/
	//m_matWorld = glm::scale(glm::vec3(2,1,2));
	
	float t = SDL_GetTicks() / ((1000.0f * 10.0f) / 3.0f);
	float parabolaTick = SDL_GetTicks() / ((1000.0f * 8.0f) / 3.0f);
	/*m_matWorld = glm::rotate(glm::radians(60.0f), glm::vec3(0,1,0)) *
		glm::translate(glm::vec3(3, 0, 0));*/

	// majd küldjük át a megfelelõ mátrixokat!
	glUniformMatrix4fv( m_loc_world,// erre a helyre töltsünk át adatot
						1,			// egy darab mátrixot
						GL_FALSE,	// NEM transzponálva
						&(m_matWorld[0][0]) ); // innen olvasva a 16 x sizeof(float)-nyi adatot
	
	for (int j = 0; j < 6; ++j) {
		
		float parabolaX = ((sinf(parabolaTick) + 1) * 7.5f) -5;
		m_matWorld = glm::translate(glm::vec3(0,0,0))
			* glm::translate(point[j])
			* glm::translate(glm::vec3(parabolaX , 0, 0.15f * pow(parabolaX,2)))
			* glm::scale(glm::vec3(1, 1, (needScale ? (((sinf(t) + 1) * 0.75f) + 0.5f) : 1)));
		//m_matWorld = glm::translate(glm::vec3(x,y,z));
		//std::cout << x << y << z;
		//alap kocka
		glDrawArrays(GL_TRIANGLES,	// rajzoljunk ki háromszöglista primitívet
			0,				// a VB elsõ eleme legyen az elsõ kiolvasott vertex
			6 * 6);				// és 6db csúcspont segítségével rajzoljunk háromszöglistát

		//függõleges copy
		for (int i = 0; i < 3; ++i) {
			m_matWorld = glm::translate(glm::vec3(0, i, 0))
				* glm::translate(point[j])
				* glm::translate(glm::vec3(parabolaX, 0, 0.15f * pow(parabolaX, 2)))
				* glm::scale(glm::vec3(1, 1, (needScale ? (((sinf(t) + 1) * 0.75f) + 0.5f) : 1)));

			// majd küldjük át a megfelelõ mátrixokat!
			glUniformMatrix4fv(m_loc_world,// erre a helyre töltsünk át adatot
				1,			// egy darab mátrixot
				GL_FALSE,	// NEM transzponálva
				&(m_matWorld[0][0])); // innen olvasva a 16 x sizeof(float)-nyi adatot

			glDrawArrays(GL_TRIANGLES,	// rajzoljunk ki háromszöglista primitívet
				0,				// a VB elsõ eleme legyen az elsõ kiolvasott vertex
				6 * 6);
		}

		//vízszíntes copy
		for (int i = 0; i < 2; ++i) {
			m_matWorld = glm::translate(glm::vec3(pow((-1.0), i), 2, 0))
				* glm::translate(point[j])
				* glm::translate(glm::vec3(parabolaX, 0, 0.15f * pow(parabolaX, 2)))
				* glm::scale(glm::vec3(1, 1, (needScale ? (((sinf(t) + 1) * 0.75f) + 0.5f) : 1)));

			// majd küldjük át a megfelelõ mátrixokat!
			glUniformMatrix4fv(m_loc_world,// erre a helyre töltsünk át adatot
				1,			// egy darab mátrixot
				GL_FALSE,	// NEM transzponálva
				&(m_matWorld[0][0])); // innen olvasva a 16 x sizeof(float)-nyi adatot


		// kapcsoljuk be a VAO-t (a VBO jön vele együtt)

		// kirajzolás
			glDrawArrays(GL_TRIANGLES,	// rajzoljunk ki háromszöglista primitívet
				0,				// a VB elsõ eleme legyen az elsõ kiolvasott vertex
				6 * 6);
		}
	}
	


	/*for (int i = 0; i < 30; ++i) {
		m_matWorld = glm::rotate(6.28f / 30.0f * i + t, glm::vec3(0,1,0))
			* glm::translate(glm::vec3(5- (cosf(t) * 5), 0, 0));

		// majd küldjük át a megfelelõ mátrixokat!
		glUniformMatrix4fv(m_loc_world,// erre a helyre töltsünk át adatot
			1,			// egy darab mátrixot
			GL_FALSE,	// NEM transzponálva
			&(m_matWorld[0][0])); // innen olvasva a 16 x sizeof(float)-nyi adatot
		

	// kapcsoljuk be a VAO-t (a VBO jön vele együtt)

	// kirajzolás
		glDrawArrays(GL_TRIANGLES,	// rajzoljunk ki háromszöglista primitívet
			0,				// a VB elsõ eleme legyen az elsõ kiolvasott vertex
			18);
	}*/

	// VAO kikapcsolasa
	glBindVertexArray(0);

	// shader kikapcsolasa
	glUseProgram( 0 );
}

void CMyApp::KeyboardDown(SDL_KeyboardEvent& key)
{
	needScale = !needScale;
}

void CMyApp::KeyboardUp(SDL_KeyboardEvent& key)
{
}

void CMyApp::MouseMove(SDL_MouseMotionEvent& mouse)
{

}

void CMyApp::MouseDown(SDL_MouseButtonEvent& mouse)
{
}

void CMyApp::MouseUp(SDL_MouseButtonEvent& mouse)
{
}

void CMyApp::MouseWheel(SDL_MouseWheelEvent& wheel)
{
}

// a két paraméterbe az új ablakméret szélessége (_w) és magassága (_h) található
void CMyApp::Resize(int _w, int _h)
{
	glViewport(0, 0, _w, _h);

	m_matProj = glm::perspective(  45.0f,		// 90 fokos nyilasszog
									_w/(float)_h,	// ablakmereteknek megfelelo nezeti arany
									0.01f,			// kozeli vagosik
									100.0f);		// tavoli vagosik
}