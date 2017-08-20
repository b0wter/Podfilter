# Podfilter
ASP .Net Core project that helps you filter your favorite podcasts. Removes items based on title, duration, pub date, ...

# Status
Filtering/modification logic is implemented and working.
Basic filtering is possible by passend special urls to the FilterController. You can pass the following parameters in the query:

  * minDuration (seconds)
  * maxDuration (seconds)
  * mustContain (string)
  * mustNotContain (string)
  * fromEpoch (seconds)
  * toEpoch (seconds)
  * url (url encoded url to the podcast)
  
An example might look like this (replace hostname and port):

  http://{{hostname}}:{{port}}/api/filter?url=http%3A%2F%2Fwww.deutschlandfunk.de%2Fpodcast-wissenschaft-im-brennpunkt.741.de.podcast.xml&titleMustNotContain=H%C3%B6rtipp&minDuration=300
  
Web interface planed but far, far away!
