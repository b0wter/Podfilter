# Podfilter
ASP .Net Core project that helps you filter your favorite podcasts. Removes items based on title, duration, pub date, ...

You simply enter the url to the podcast, select some filters and get a new url that filters the podcast on the fly.

# Status
Project is finished. Please report any bugs you find in the issue tracker. If you have any feature request please use the issue tracker as well. To see it in action simply go here:

http://podfilterweb.azurewebsites.net/

note that this sample runs on a free Azure tier and might not always be available and suffer from poor performance.

# Docker
Project includes a docker file. To build use:

	docker build -t podfilterweb .  

and run as you please. E.g. with:

	sudo docker run -d -p 8099:80 --name mypodfilter podfilterweb

Replace port 8099 how you see fit.
