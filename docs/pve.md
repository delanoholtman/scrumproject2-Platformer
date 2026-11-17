# Programma van Eisen

## Contents
- [Introduction](#introduction)
- [Functional requirements](#functional-requirements)
- [Technologies](#technologies)
- [Task assignment](#task-assignment)
- [Eisen en wensen (MoSCoW)](#MoSCoW)

## Introduction
PixelQuest._ wil hun medewerkers de mogelijkheid geven om zich te ontspannen en creatief bezig te zijn met een intern ontwikkelde game.

We bouwen een eenvoudige 2D-platformgame waarin medewerkers hun eigen avatar kunnen besturen door een pixelwereld vol hindernissen. Spelers kunnen springen, lopen en obstakels ontwijken. Daarnaast krijgen gebruikers basis level-design tools waarmee ze hun eigen levels kunnen ontwerpen en delen met collega's. Per level wordt een scorebord bijgehouden op basis van score en tijd, zodat spelers hun prestaties met elkaar kunnen vergelijken. De game krijgt een pixel art stijl, passend bij de huidige trend van retro games.

## Functional requirements
- Karakterbesturing: de speler kan de avatar laten springen, lopen en obstakels ontwijken
- Basis level-design tools waarmee gebruikers eigen levels kunnen ontwerpen
- De mogelijkheid om zelfgemaakte levels te delen met collega's
- Score- en tijdsregistratie per level
- Een scorebord (leaderboard) per level waarop spelers hun prestaties kunnen vergelijken
- Pixel art vormgeving, consistent door de hele game

## Technologies
- Unity
- C#

## Eisen en wensen (MoSCoW)

### Must-haves
- Werkende karakterbesturing (springen, lopen, botsen met obstakels)
- Pixel art visuals
- Obstakels waar de speler doorheen/overheen moet navigeren
- Basic level-editor (levels kunnen worden opgebouwd en opgeslagen)
    - inventory systeem
    - grid systeem
        - click and place
    - obstakels
        - spikes/spinners
        - basic platforms


- Scorebord per level, zichtbaar voor spelers
- Levels moeten gedeeld kunnen worden met collegas
- Level opslag systeem
- level selecter
- start/eind scherm/menu
- editor/player mode
- sharing system (how is pending)

### Should-haves
- Score- en tijdregistratie per level
- Levels delen via een link of interne galerij
- Geluidseffecten en achtergrondmuziek in retro-stijl
- obsakles
    - checkpoints
    - turret
    - moving platform


### Could-haves
- Meerdere pixel art skins/avatars om uit te kiezen
- Mogelijkheid om levels van collega's te beoordelen (likes/rating)
- Safe-points op andere elke levels
- Website pagina over de game
- dash
- power ups
- enemies
- outomatische sprite tiling
- meerdere levels

### Wont-haves
- score en tijd opslaan in database
- 3d game
- animatie