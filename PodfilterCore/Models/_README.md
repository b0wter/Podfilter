Content Actions
---------------
ContentActions are actions that are performed on values rather than the podcast itself.
That means that in order to use a ContentAction you need to retrieve a XElement from a pocast first and then retrieve its value for an evaluation.
An example for an action is the AddStringContentAction which simply adds a pre- and/or a suffix to a given string.

Contant Filter
--------------
A ContentFilter<T> parses a given value and returns wether or not the value passes the filter.
That means that in order to use a ContentFilter you need to retrieve a XElement from a pocast first and then retrieve its value for an evaluation.
An example of a ContentFilter is the StringFilter with which you can remove elements from a podcast that contain a certain string in their title.

IPodcastElementProvider
-----------------------
Takes an XDocument and retrieves a set of XElements.
There is only one implementation of this interface: XPathPodcastElementProvider which selects XElements using an xpath selector.

Podcast Modification
--------------------
A podcast modification glues together a content action/filter and an element provider.