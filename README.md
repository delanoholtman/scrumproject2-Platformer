# PixelQuest

Een 2D-platformer in pixel art, gemaakt in Unity. Spring en ren door levels vol obstakels, bouw je eigen levels in de ingebouwde level-editor (een beetje zoals Mario Maker) en laat collega's je levels spelen.

## Inhoudsopgave

- [Ons idee](#ons-idee)
- [Voor wie?](#voor-wie)
- [Functionaliteit](#functionaliteit)
- [Besturing](#besturing)
- [Levels bouwen en delen](#levels-bouwen-en-delen)
- [Technologie](#technologie)
- [Projectstatus](#projectstatus)
- [Mapstructuur](#mapstructuur)
- [Team en taakverdeling](#team-en-taakverdeling)

## Ons idee

PixelQuest wil medewerkers de mogelijkheid geven om zich te ontspannen en creatief bezig te zijn met een intern ontwikkelde game.

Het is een 2D-platformer met parkour-gameplay. De speler bestuurt een avatar door een pixelwereld vol hindernissen en vijanden. Daarnaast kan iedereen met de level-editor eigen levels ontwerpen, opslaan en delen met collega's. De score is gebaseerd op tijd, zodat spelers hun prestaties met elkaar kunnen vergelijken.

De game heeft een pixel art stijl, passend bij de huidige trend van retro games.

## Voor wie?

PixelQuest is bedoeld voor medewerkers van PixelQuest die willen ontspannen met een korte game en zelf creatief aan de slag willen met level-design.

## Functionaliteit

- **2D-platformer:** lopen, springen en obstakels ontwijken.
- **Vijanden:** er zijn 2 soorten enemies die naar links en rechts kunnen bewegen ze kunnen niet springen en maken je dood op contact
- **Obstakels:** onder andere spikes en spinners.
- **Level-editor:** bouw je eigen level met een grid, een inventory en "click and place".
- **Opslaan:** sla je gebouwde level op en werk er later verder aan.
- **Levels delen:** andere spelers kunnen jouw level spelen.
- **Score op basis van tijd:** De score is gebaseerd op tijd en het wordt opgeslagen.
- **Menu's:** start- en eindscherm en een level selecter.
- **Editor- en speelmodus:** schakel eenvoudig tussen bouwen en spelen om je level direct te testen.
- **Pixel art:** een consistente vormgeving door de hele game.

## Besturing

| Toets | Actie |
|-------|-------|
| `A` | Naar links lopen |
| `D` | Naar rechts lopen |
| `Spatiebalk` | Springen |

In de level-editor plaats je objecten met je muis door ze uit de inventory te kiezen en op het grid te klikken.

## Levels bouwen en delen

1. Kies in het menu de **editor-modus**.
2. Selecteer een blok, platform of obstakel in de inventory.
3. Klik op het grid om het object te plaatsen.
4. Sla je level op.
5. Schakel naar de **speelmodus** om je level te testen.
6. Deel je levelbestand met collega's. Zij kunnen het openen via de level selecter en jouw level spelen.

## Technologie

- **Unity:** de game-engine waarin de game wordt gebouwd.
- **C#:** de programmeertaal voor de gamelogica.
- **Git:** versiebeheer en samenwerking binnen het team.

## Projectstatus

**Werkt al**
- Karakterbesturing (lopen en springen)
- Level-editor met opslaan
- Levels van anderen kunnen spelen
- 2 enemies
- Score op basis van tijd
- werkende versie

## taakverdeling

- lars = gridsystem+cameramovement
- mallik = blok functionality+playermovement
- delano = save+loadsystem+sprites
