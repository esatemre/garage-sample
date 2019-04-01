What architectures or patterns are you using currently or have worked on recently?
- SOLID principles, Domain Driven Design, Event Sourcing, CQRS, Microservices, Model Driven Engineering

What do you think of them and would you want to implement it again?
- I would like to do but it is really depending on the project.

What version control system do you use or prefer?

- It doesn't matter for me. I have used Git and Team Foundation Server.
 
What is your favorite language feature and can you give a short snippet on how you use it?

- prop+TAB+TAB :) AutoProperty Initializer. Best thing ever for me. Unlike Java, you don't have to write getter and setter. Of course, there are more complex samples for C#. But this is the simplest and first one for me.

What future or current technology do you look forward to the most or want to use and why?

- Mixed Reality Glasses and IOTA (cryptocurrency). They gonna definitely change our daily life.
 
How would you find a production bug/performance issue? Have you done this before?

- I was debugging the code while connected to the production database. I know, this is the worst case scenario :) But I had to do on that time. Best solution is checking commits between deployments and preparing test scenarios for possible cases. For performance issues, there are Profilers or Application Insights (It is much more useful in Azure).
 
How would you improve the sample API (bug fixes, security, performance, etc.)?

- Static user model could be changed. User and Garage is always number 2 for current cases. There is no user-password check. It could be implemented. I have added Cross Origin Resource Sharing as allowing all. I could specify frontend addresses for CORS in the appsettings. 
Logging should be added when the project gets bigger. For getting and updating door information, We can use SignalR, CQRS, EventSourcing and Hangfire for background jobs.
