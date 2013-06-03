# Save Empire Earth Lobby

[http://www.save-ee.com/](http://www.save-ee.com/)

The Save-EE Lobby is an online multiplayer lobby for _Empire Earth_ and the
_Art of Conquest Expansion_.

Empire Earth is a real-time strategy PC game that was developed by Stainless
Steel Studios and published by Sierra Entertainment in 2001.  Since its
release, Sierra and its parent company have been bought by Activision.  In late
2008, Activision announced that it was shutting down the multiplayer servers
that were dedicated to many of Sierra's old games, including Empire Earth.

### About the Application

The Save-EE Lobby is a very basic client-server application programmed in
Visual Basic .NET using the .NET 2.0 Framework.  It consists of a client
application that is available for download from the website, and a server
application that is hosted on a virtual private server (VPS).  The clients and
the server communicate via "encrypted" socket programming.

The client application consists solely of an executable file and was designed
to maintain the familiar look of Empire Earth's multiplayer lobby.  It includes
nearly every feature of the old lobby and then some.  It features automatic
updating and a system that detects the game version and updates it so that all
players are using the same version.  There is a full-featured chat lobby and
friends list.  The lobby also uses some low-level packet sniffing to obtain a
list of games from connected clients that are hosting (via the game's
connect-by-IP feature) and it also detects players that join the games.

The server application uses a listener to accept incoming connections and then
adds them to a list of sessions.  Each session then handles each client
connection and can broadcast to other appropriate sessions as needed.  The
server features a full log system and an intricate user-banning system, which
can be controlled by administrators via the client application.

### About the Operation

Since its inception in October of 2008, the Save-EE project has grown into a
central community for online Empire Earth players.  Save-EE runs solely on
donations from the community and the free time of the staff members.  All
donations are put toward the hosting costs of the VPS and website.

### Authors

- Richard Lange (up to client version 2.5.0)
- Matt DeTullio (client versions after 2.5.0)