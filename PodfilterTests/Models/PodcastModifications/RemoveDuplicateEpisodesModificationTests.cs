using FakeItEasy;
using Podfilter.Models.PodcastModification.Others;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Xunit;

namespace PodfilterTests.Models.PodcastModifications
{
    /// <summary>
    /// Modification that removes duplicate episodes from the podcast. Duplicates are identified by title.
    /// </summary>
    public class RemoveDuplicateEpisodesModificationTests
    {
        private XDocument CreateFakePodcastWithDuplicateElements()
        {
            var podcast = XDocument.Parse(FakePodcastWithDuplicateElements);
            return podcast;
        }

        [Fact]
        public void Modify_WithTestArgument_RemovesDuplicates()
        {
            var podcast = CreateFakePodcastWithDuplicateElements();
            var modification = new RemoveDuplicateEpisodesModification();

            modification.Modify(podcast);

            Assert.Equal(3, podcast.Descendants("item").Count());
        }

        [Fact]
        public void Modify_WithNullArgument_ThrowsArgumentNullException()
        {
            var modification = new RemoveDuplicateEpisodesModification();

            Assert.Throws<ArgumentNullException>(() => modification.Modify(null));
        }

        private string FakePodcastWithDuplicateElements => 
            "<?xml version=\"1.0\" encoding=\"utf-8\"?> <?xml-stylesheet type=\"text/xsl\" media=\"screen\" href=\"http://www.deutschlandfunk.de/themes/dradio/podcast/podcast.xsl\"?><rss xmlns:itunes=\"http://www.itunes.com/dtds/podcast-1.0.dtd\" xmlns:atom=\"http://www.w3.org/2005/Atom\" version=\"2.0\"> <channel> <title>Nachrichten - Deutschlandfunk</title> <link>http://www.deutschlandfunk.de/die-nachrichten.353.de.html</link> <atom:link rel=\"self\" type=\"application/rss+xml\" href=\"http://www.deutschlandfunk.de/podcast-nachrichten.1257.de.podcast.xml\"/> <description>Ausgewählte aktuelle Beiträge aus dem Angebot vom Deutschlandfunk</description> <category>Info</category> <copyright>Deutschlandradio - deutschlandradio.de</copyright> <ttl>60</ttl> <language>de-DE</language> <pubDate>Mon, 21 Aug 2017 18:07:38 +0200</pubDate> <lastBuildDate>Mon, 21 Aug 2017 18:07:38 +0200</lastBuildDate> <image> <url>http://www.deutschlandfunk.de/media/files/2/23407846a86cd6b006ea11ace8e0c4a5v3.jpg</url> <title>Nachrichten - Deutschlandfunk</title> <link>http://www.deutschlandfunk.de/die-nachrichten.353.de.html</link> <description>Feed provided by Deutschlandradio. Click to visit.</description> </image> <itunes:subtitle>Die Beiträge zur Sendung</itunes:subtitle> <itunes:image href=\"http://www.deutschlandfunk.de/media/files/2/23407846a86cd6b006ea11ace8e0c4a5v3.jpg\"/> <itunes:new-feed-url>http://www.deutschlandfunk.de/podcast-nachrichten.1257.de.podcast.xml</itunes:new-feed-url> <itunes:owner> <itunes:name>Redaktion deutschlandradio.de</itunes:name> <itunes:email>podcast@deutschlandradio.de</itunes:email> </itunes:owner> <itunes:author>Deutschlandfunk</itunes:author> <itunes:explicit>No</itunes:explicit> <itunes:category text=\"News &amp; Politics\"/> " +
            "<item> <title>Nachrichten</title> <link>http://podcast-mp3.dradio.de/podcast/2017/08/20/nachrichten_dlf_20170820_1700_b228606a.mp3</link> <description><![CDATA[Autor: Deutschlandfunk-Nachrichtenredaktion<br/>Sendung: <a href=\"http://www.deutschlandfunk.de/nachrichten.353.de.html\">Nachrichten</a><br/>Hören bis: 27.08.2017 17:00<br/><br/>]]></description> <guid>http://podcast-mp3.dradio.de/podcast/2017/08/20/nachrichten_dlf_20170820_1700_b228606a.mp3</guid> <pubDate>Mon, 21 Aug 2017 17:00:00 +0200</pubDate> <enclosure url=\"http://podcast-mp3.dradio.de/podcast/2017/08/20/nachrichten_dlf_20170820_1700_b228606a.mp3\" length=\"3817351\" type=\"audio/mpeg\"/> <itunes:author>Deutschlandfunk-Nachrichtenredaktion</itunes:author> <itunes:duration>03:55</itunes:duration> </item> " +
            "<item> <title>Nachrichten</title> <link>http://podcast-mp3.dradio.de/podcast/2017/08/20/nachrichten_dlf_20170820_1600_7df170bf.mp3</link> <description><![CDATA[Autor: Deutschlandfunk-Nachrichtenredaktion<br/>Sendung: <a href=\"http://www.deutschlandfunk.de/nachrichten.353.de.html\">Nachrichten</a><br/>Hören bis: 27.08.2017 16:00<br/><br/>]]></description> <guid>http://podcast-mp3.dradio.de/podcast/2017/08/20/nachrichten_dlf_20170820_1600_7df170bf.mp3</guid> <pubDate>Sun, 20 Aug 2017 16:00:01 +0200</pubDate> <enclosure url=\"http://podcast-mp3.dradio.de/podcast/2017/08/20/nachrichten_dlf_20170820_1600_7df170bf.mp3\" length=\"7873979\" type=\"audio/mpeg\"/> <itunes:author>Deutschlandfunk-Nachrichtenredaktion</itunes:author> <itunes:duration>08:09</itunes:duration> </item> " +
            "<item> <title>Nachrichten</title> <link>http://podcast-mp3.dradio.de/podcast/2017/08/20/nachrichten_dlf_20170820_1500_46ba7b87.mp3</link> <description><![CDATA[Autor: Deutschlandfunk-Nachrichtenredaktion<br/>Sendung: <a href=\"http://www.deutschlandfunk.de/nachrichten.353.de.html\">Nachrichten</a><br/>Hören bis: 27.08.2017 15:00<br/><br/>]]></description> <guid>http://podcast-mp3.dradio.de/podcast/2017/08/20/nachrichten_dlf_20170820_1500_46ba7b87.mp3</guid> <pubDate>Sun, 20 Aug 2017 15:00:00 +0200</pubDate> <enclosure url=\"http://podcast-mp3.dradio.de/podcast/2017/08/20/nachrichten_dlf_20170820_1500_46ba7b87.mp3\" length=\"3762307\" type=\"audio/mpeg\"/> <itunes:author>Deutschlandfunk-Nachrichtenredaktion</itunes:author> <itunes:duration>03:51</itunes:duration> </item> " +
            "<item> <title>Nachrichten</title> <link>http://podcast-mp3.dradio.de/podcast/2017/08/20/nachrichten_dlf_20170820_1400_c3839246.mp3</link> <description><![CDATA[Autor: Deutschlandfunk-Nachrichtenredaktion<br/>Sendung: <a href=\"http://www.deutschlandfunk.de/nachrichten.353.de.html\">Nachrichten</a><br/>Hören bis: 27.08.2017 14:00<br/><br/>]]></description> <guid>http://podcast-mp3.dradio.de/podcast/2017/08/20/nachrichten_dlf_20170820_1400_c3839246.mp3</guid> <pubDate>Sun, 20 Aug 2017 14:00:00 +0200</pubDate> <enclosure url=\"http://podcast-mp3.dradio.de/podcast/2017/08/20/nachrichten_dlf_20170820_1400_c3839246.mp3\" length=\"3690166\" type=\"audio/mpeg\"/> <itunes:author>Deutschlandfunk-Nachrichtenredaktion</itunes:author> <itunes:duration>03:47</itunes:duration> </item> " +
            "<item> <title>Nachrichten</title> <link>http://podcast-mp3.dradio.de/podcast/2017/08/20/nachrichten_dlf_20170820_1300_3b4ca145.mp3</link> <description><![CDATA[Autor: Deutschlandfunk-Nachrichtenredaktion<br/>Sendung: <a href=\"http://www.deutschlandfunk.de/nachrichten.353.de.html\">Nachrichten</a><br/>Hören bis: 27.08.2017 13:00<br/><br/>]]></description> <guid>http://podcast-mp3.dradio.de/podcast/2017/08/20/nachrichten_dlf_20170820_1300_3b4ca145.mp3</guid> <pubDate>Sun, 20 Aug 2017 13:00:00 +0200</pubDate> <enclosure url=\"http://podcast-mp3.dradio.de/podcast/2017/08/20/nachrichten_dlf_20170820_1300_3b4ca145.mp3\" length=\"3789829\" type=\"audio/mpeg\"/> <itunes:author>Deutschlandfunk-Nachrichtenredaktion</itunes:author> <itunes:duration>03:53</itunes:duration> </item> " +
            "<item> <title>Nachrichten</title> <link>http://podcast-mp3.dradio.de/podcast/2017/08/20/nachrichten_dlf_20170820_1200_b515b511.mp3</link> <description><![CDATA[Autor: Deutschlandfunk-Nachrichtenredaktion<br/>Sendung: <a href=\"http://www.deutschlandfunk.de/nachrichten.353.de.html\">Nachrichten</a><br/>Hören bis: 27.08.2017 12:00<br/><br/>]]></description> <guid>http://podcast-mp3.dradio.de/podcast/2017/08/20/nachrichten_dlf_20170820_1200_b515b511.mp3</guid> <pubDate>Sun, 20 Aug 2017 12:00:00 +0200</pubDate> <enclosure url=\"http://podcast-mp3.dradio.de/podcast/2017/08/20/nachrichten_dlf_20170820_1200_b515b511.mp3\" length=\"7671734\" type=\"audio/mpeg\"/> <itunes:author>Deutschlandfunk-Nachrichtenredaktion</itunes:author> <itunes:duration>07:56</itunes:duration> </item> " +
            "<item> <title>Nachrichten</title> <link>http://podcast-mp3.dradio.de/podcast/2017/08/20/nachrichten_dlf_20170820_1100_61dec933.mp3</link> <description><![CDATA[Autor: Deutschlandfunk-Nachrichtenredaktion<br/>Sendung: <a href=\"http://www.deutschlandfunk.de/nachrichten.353.de.html\">Nachrichten</a><br/>Hören bis: 27.08.2017 11:00<br/><br/>]]></description> <guid>http://podcast-mp3.dradio.de/podcast/2017/08/20/nachrichten_dlf_20170820_1100_61dec933.mp3</guid> <pubDate>Sun, 20 Aug 2017 11:00:00 +0200</pubDate> <enclosure url=\"http://podcast-mp3.dradio.de/podcast/2017/08/20/nachrichten_dlf_20170820_1100_61dec933.mp3\" length=\"3658109\" type=\"audio/mpeg\"/> <itunes:author>Deutschlandfunk-Nachrichtenredaktion</itunes:author> <itunes:duration>03:45</itunes:duration> </item> " +
            "<item> <title>Nachrichten</title> <link>http://podcast-mp3.dradio.de/podcast/2017/08/20/nachrichten_dlf_20170820_1000_f7a7d65e.mp3</link> <description><![CDATA[Autor: Deutschlandfunk-Nachrichtenredaktion<br/>Sendung: <a href=\"http://www.deutschlandfunk.de/nachrichten.353.de.html\">Nachrichten</a><br/>Hören bis: 27.08.2017 10:00<br/><br/>]]></description> <guid>http://podcast-mp3.dradio.de/podcast/2017/08/20/nachrichten_dlf_20170820_1000_f7a7d65e.mp3</guid> <pubDate>Sun, 20 Aug 2017 10:00:00 +0200</pubDate> <enclosure url=\"http://podcast-mp3.dradio.de/podcast/2017/08/20/nachrichten_dlf_20170820_1000_f7a7d65e.mp3\" length=\"4173104\" type=\"audio/mpeg\"/> <itunes:author>Deutschlandfunk-Nachrichtenredaktion</itunes:author> <itunes:duration>04:17</itunes:duration> </item> " +
            "<item> <title>Nachrichten</title> <link>http://podcast-mp3.dradio.de/podcast/2017/08/20/nachrichten_dlf_20170820_0900_1c70ed5e.mp3</link> <description><![CDATA[Autor: Deutschlandfunk-Nachrichtenredaktion<br/>Sendung: <a href=\"http://www.deutschlandfunk.de/nachrichten.353.de.html\">Nachrichten</a><br/>Hören bis: 27.08.2017 09:00<br/><br/>]]></description> <guid>http://podcast-mp3.dradio.de/podcast/2017/08/20/nachrichten_dlf_20170820_0900_1c70ed5e.mp3</guid> <pubDate>Sun, 20 Aug 2017 09:00:00 +0200</pubDate> <enclosure url=\"http://podcast-mp3.dradio.de/podcast/2017/08/20/nachrichten_dlf_20170820_0900_1c70ed5e.mp3\" length=\"4082615\" type=\"audio/mpeg\"/> <itunes:author>Deutschlandfunk-Nachrichtenredaktion</itunes:author> <itunes:duration>04:11</itunes:duration> </item> " +
            "<item> <title>Nachrichten</title> <link>http://podcast-mp3.dradio.de/podcast/2017/08/20/nachrichten_dlf_20170820_0830_685570f7.mp3</link> <description><![CDATA[Autor: Deutschlandfunk-Nachrichtenredaktion<br/>Sendung: <a href=\"http://www.deutschlandfunk.de/nachrichten.353.de.html\">Nachrichten</a><br/>Hören bis: 27.08.2017 08:30<br/><br/>]]></description> <guid>http://podcast-mp3.dradio.de/podcast/2017/08/20/nachrichten_dlf_20170820_0830_685570f7.mp3</guid> <pubDate>Sat, 19 Aug 2017 08:30:00 +0200</pubDate> <enclosure url=\"http://podcast-mp3.dradio.de/podcast/2017/08/20/nachrichten_dlf_20170820_0830_685570f7.mp3\" length=\"4236071\" type=\"audio/mpeg\"/> <itunes:author>Deutschlandfunk-Nachrichtenredaktion</itunes:author> <itunes:duration>04:21</itunes:duration> </item> " +
            "</channel> </rss>";
    }
}
