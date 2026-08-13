# Auction House
The Auction House project was originally came as an idea during a regular poker night session with some friends. 
Ultimately, the application will be a central place where users can login and be ready to participate in some of their
favorite activities. 

A console application to demonstrate a simple Auction House (soon to be casino) where Users can bid on auctions, and eventually have a chance to play poker.



### Structure
Application follows a Clean/Layered Architecture, where dependencies only point inward, toward the domain.  
  
There are four different projects in the solution: Domain, Infrastructure, Application, API.  
  
API -> Application -> Domain  
Infrastructure -> Domain 
  
### Domain  
Domain is the core of the project and it references no other projects. All it contains is business is business rules for the different entities.
For example, a bid cannot be placed if the current bid is more than the one trying to be placed.  
  
### Application  
References Domain only because this is where use cases for the application: Creating a user, creating an auction, and placing a bid. 
Each of the services that are implemented take a repository interface and saves it's entities through that interface. 
  
### Infrastructure
References Domain, implements the repository interfaces that were declared in the domain.
  
#### API 
References both Application and Infrastructure. Program is the composition root, or the main entry point. It registers the 
Infrastructure classes against the Domain interfaces. The endpoints ask for the interface/service and the API will hand the real 
Implementation of them. 
  
  
### Takeaways 
This was a big learning opportunity for me and tested my programming knowledge very well. The biggest takeaway that I had from this 
project was the Dependency flow in the project. I abstraction is a very fundamental concept in not just programming in general
but also in object-orientated programming that I love to see in action. As an example, the domain defines the IUserRepository
interface, but it doesn't know or care about how it is current and in-memory dictionary. But because I made the interface, I can freely change the way 
the data storage method without changing anything about the domain.


### Roadmap 

#### Now / in progress 
- Finish CreateUserService + wire the POST /users endpoint
- Flesh GET/list endpoints for auctions and bids.

#### Near 
- Validation of input for the validation. So if the email, starting price, bid amount, aren't valid. Should be done at the API level.
- Proper error handling
- More unit tests
- Authentications

#### Middle of the road
- Swap in-memory repositories for a real database
- Automatically adjust auctions instead of doing requests. (An auction can expire, shouldn't need a request for that)

#### Long-term
- Implement real-time updates for auctions
- Implement UI 
- Full gambling house features (Poker, Blackjack, etc.).