
# MLAgents III Jumper

*Project door: Lowie Van Meensel en Vislan Kagermanov*

## Inleiding

Dit project maakt gebruik van ML-Agents in Unity om een zelflerende agent te implementeren. De agent leert obstakels te ontwijken door erover te springen. In elke episode varieert de snelheid van de obstakels, wat de uitdaging vergroot. Daarnaast zijn er obstakels die bonuspunten opleveren bij een botsing, terwijl andere obstakels juist vermeden moeten worden.

## Methoden

**Requirements**

- Unity 6 (6000.0.36f1)
- Unity: ML-Agents (Unity Package Manager)
- Anaconda (v24.xx.xx)

**Agent Eigenschappen**

  - Kern observatie: Raycast detectie van de omgeving
  - Andere observaties:
    - Of de agent op de grond is (en dus kan springen)
    - Positie en snelheid van het obstakel/reward
    - Type object dat op de agent afkomt (obstacle of reward)
  - DiscreteAction: Springen (1) of niet springen (0)

**Reward Systeem**

- Springen: **\-0.01**
- Niet springen: **0.005**
- In leven blijven: **0.01**
- Succesvol over een obstakel springen: **+1.0**
- Drie obstakels na elkaar correct springen: **+0.5**
- Over een reward springen: **\-0.5**
- Een reward pakken: **+0.5**
- Obstakel raken: **\-1.0**

Kleine veranderingen in de waarde van deze beloningen hebben een grote impact op het leerproces. Bijvoorbeeld, het weglaten van de negatieve spring-beloning resulteerde in een agent die voortdurend sprong, terwijl een te grote negatieve spring-beloning ervoor zorgde dat de agent bijna niet sprong. Een 2<sup>de</sup> voorbeeld is dat het verhogen van de beloning voor afstand tot doel verminderen kan ervoor zorgen dat de agent agressiever naar het doel toe beweegt. Aan de andere kant, als de beloning voor buiten de grenzen bewegen te laag is, kan de agent onbedoeld de grenzen negeren, wat leidt tot slechtere prestaties in het leren van de taak. Het zorgvuldig afstemmen van deze beloningen is dus cruciaal voor het succes van de agent.

**Setup**

(Voor de training van de agent is een Python-omgeving nodig. Hiervoor is dient Anaconda al geinstalleerd te zijn.)

1. Maak een nieuwe omgeving aan in Anaconda prompt en installeer in deze nieuwe omgeving de ML-Agents package:

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;conda create --name mlagents python=3.9.18

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;conda activate mlagents

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;pip3 install torch~=1.7.1 -f <https://download.pytorch.org/whl/torch_stable.html>

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;pip install protobuf==3.20.\*

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;python -m pip install mlagents==0.30.0

(Voor het trainen van de agent is een configuratiebestand vereist.)

2. Maak een yaml bestand aan en noem deze Agent.yaml, plaats vervolgens deze bestand in de config folder van je project.

(De Unity Scene is al opgezet, zie hiervoor de git repo)

3. Start de training door op in de Anaconda prompt deze commando uit te voeren (vervolgens druk je op Play in de Unity Editor):

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;mlagents-learn config/Agent.yaml --force --run-id=Agent

(Ten slotte kan je ook de trainingprogressie visueel weergeven door de command ''tensorboard --logdir results'' in een nieuwe prompt)

## Resultaten

De resultaten zijn te zien op de onderstaande tensorboard:

ZIE LINK: https://imgur.com/a/tensorboard-jumper-kJdQtnA

(Blauwe lijn: training waarbij de agent extra observaties kreeg)
(Oranje lijn: de training observaties)

## Conclusie

Zoals je kan al hierboven op de figuur kan opmerken door de toevoeging van deze extra observaties leerde de agent aanzienlijk sneller en effectiever obstakels te vermijden.

Door de rewards en observaties zorgvuldig af te stemmen, werd een efficiënte agent getraind. De belangrijkste inzichten waren:

- Evenwicht in rewards: Zowel overmatige straffen als beloningen kunnen het gedrag van de agent sterk beïnvloeden.
- Toegevoegde observaties: Specifieke observaties speelden een cruciale rol in het versnellen van de leercurve.


