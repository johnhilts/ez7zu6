# ez7zu6
Easy 地図録!

A web app to make it easy to record experiences.

**Planned features**
- Geo-Location
- Image Upload
- Current Weather 
- Calendar
- Social Graph
- Text Search 
- Location Search
- Privacy with White listing

**Tech Stack**
- asp.net core 2 (web api)
- reactjs
### Testing
- nspec
- jest
- mocha
- cypress.io

_Why so many test frameworks?_
"Best tool for the job"
nspec for C# ... is the only testing framework that supports the context/specification style which is exremely common in the ruby and JavaScript communities.
jest for (usually pure) react components - it takes a snapshot and is very easy to check for regressions. A great example of using mock objects, the best part being that the developer doesn't even have to set the mocks up - they're completely transparent!
mocha for everything else in JavaScript - because jest is too slow (at least for me)
cypress - an extrememly fast e2e testing framework that avoids the limitations of WebDriver. It's super fast, and you don't even have to add waits - they're handled automatically!
