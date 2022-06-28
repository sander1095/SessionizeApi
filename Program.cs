// See https://aka.ms/new-console-template for more information
using HtmlAgilityPack;

var sessions = GetSessions();
var events = GetEvents();


Console.WriteLine("-----------------");
Console.WriteLine("Sessions:");
foreach(var session in sessions)
{
    Console.WriteLine($"Title: {session.Title}\tURL: {session.SessionzeUri}");
}
Console.WriteLine("-----------------");
Console.WriteLine("Events:");
foreach (var @event in events)
{
    Console.WriteLine(@event);
}
Console.WriteLine("-----------------");


static List<Session> GetSessions()
{
    // https://sessionize.com/api/speaker/sessions/35d4ba9f-dd85-4851-b551-162d84ad570e/0x1x3fb393x
    var rawHtml = "document.write('\u003cdiv class=\"sz-root\"\u003e \u003cdiv class=\"sessionize-wrapper \" id=\"sessionize\"\u003e \u003ch2 class=\"sz-caption\"\u003e \u003csvg width=\"32\" height=\"32\" xmlns=\"http://www.w3.org/2000/svg\"\u003e\u003cpath d=\"M27.06 2.43l-4.727 4.645a.532.532 0 0 1-.743 0 .51.51 0 0 1 0-.73L26.318 1.7a.532.532 0 0 1 .743 0 .51.51 0 0 1 0 .73zm-8.349-1.09l-.869 2.792a.527.527 0 0 1-.655.343.515.515 0 0 1-.35-.645l.87-2.792a.527.527 0 0 1 .656-.342.515.515 0 0 1 .348.644zm9.023 9.55l-2.842.854a.527.527 0 0 1-.656-.343.515.515 0 0 1 .349-.644l2.842-.854a.527.527 0 0 1 .656.343.515.515 0 0 1-.349.644zM22.95.953l-2.678 4.38a.53.53 0 0 1-.721.176.511.511 0 0 1-.18-.708L22.05.42a.53.53 0 0 1 .721-.176c.249.146.33.464.18.708zm5.412 5.67l-4.458 2.632a.53.53 0 0 1-.72-.177.511.511 0 0 1 .18-.708L27.82 5.74a.53.53 0 0 1 .72.176.511.511 0 0 1-.179.708zm-14.28 19.7C7.488 26.323 2 20.93 2 14.452 2 7.793 7.14 2.58 14.082 2.58h.776l-.288.707-6.304 15.484-.116-.556 2.101 2.064-.566-.114 4.251-1.67.367-.145.243.306 2.549 3.22-.703-.115 1.605-1.032-.183.66-1.605-3.189-.252-.5.528-.208 1.4-.55 3.08-1.21 3.079-1.21 1.015-.4.284-.111.1-.04.721-.283v.763c0 7.163-5.11 11.87-12.082 11.87zm-5.295 1.564a9.025 9.025 0 0 1-.138-.047.514.514 0 0 1-.314-.662c.1-.267.4-.406.674-.308a22.7 22.7 0 0 0 1.692.501c.804.204 1.591.357 2.324.437.374.042.728.063 1.057.063.33 0 .683-.021 1.058-.063.732-.08 1.52-.233 2.324-.437a22.7 22.7 0 0 0 1.692-.501.528.528 0 0 1 .673.308.514.514 0 0 1-.314.662l-.138.047a23.759 23.759 0 0 1-1.65.483c-.85.216-1.684.378-2.47.464-.412.046-.805.07-1.175.07s-.763-.024-1.174-.07a18.303 18.303 0 0 1-2.47-.463 23.759 23.759 0 0 1-1.65-.484zm1.759 3.143a6.327 6.327 0 0 1-.307-.089.514.514 0 0 1-.332-.653.527.527 0 0 1 .664-.326 11.222 11.222 0 0 0 .988.242 13.14 13.14 0 0 0 2.523.248 13.14 13.14 0 0 0 3.26-.418c.126-.033.21-.059.251-.072a.527.527 0 0 1 .665.326.514.514 0 0 1-.333.653c-.058.02-.162.05-.307.09a14.2 14.2 0 0 1-3.536.454 14.2 14.2 0 0 1-3.536-.455z\" fill-rule=\"nonzero\"\u003e\u003c/path\u003e\u003c/svg\u003e Sander\u0027s Sessions \u003c/h2\u003e \u003cul class=\"sz-group\"\u003e \u003cli class=\"sz-item\"\u003e \u003ca href=\"https://sessionize.com/s/sander-ten-brinke/learning-to-hateoas/48315\" class=\"sz-item__title\" target=\"_blank\"\u003eLearning to ❤️ HATEOAS\u003c/a\u003e \u003c/li\u003e \u003cli class=\"sz-item\"\u003e \u003ca href=\"https://sessionize.com/s/sander-ten-brinke/keep-it-secret-keep-it-safe-with-.net/48314\" class=\"sz-item__title\" target=\"_blank\"\u003eKeep it secret, keep it safe with .NET!\u003c/a\u003e \u003c/li\u003e \u003c/ul\u003e \u003cdiv class=\"sz-powered-by\"\u003e \u003ca href=\"https://sessionize.com/\" target=\"_blank\"\u003epowered by \u003cstrong\u003eSessionize.com\u003c/strong\u003e\u003c/a\u003e \u003c/div\u003e \u003c/div\u003e\u003c/div\u003e');";
    var UnescapedHtml = Uri.UnescapeDataString(rawHtml);
    var sanitizedHtml = UnescapedHtml.Replace("document.write('", "").Replace("');", "");

    var doc = new HtmlDocument();
    doc.LoadHtml(sanitizedHtml);
    var rawSessions = doc.DocumentNode.SelectNodes("//ul/li");

    var sessions = new List<Session>();
    foreach (var session in rawSessions)
    {
        var sessionTitle = session.InnerText;
        var sessionUri = new Uri(session.SelectSingleNode("//a").Attributes["href"].Value);

        sessions.Add(new(sessionTitle, sessionUri));
    }

    return sessions;
}

static List<Event> GetEvents()
{
    // https://sessionize.com/api/speaker/events/35d4ba9f-dd85-4851-b551-162d84ad570e/0x1x3fb393x
    var rawHtml = "document.write('\u003cdiv class=\"sz-root\"\u003e \u003cdiv class=\"sessionize-wrapper \" id=\"sessionize\"\u003e \u003ch2 class=\"sz-caption\"\u003e \u003csvg width=\"32\" height=\"32\" xmlns=\"http://www.w3.org/2000/svg\"\u003e\u003cpath d=\"M27.06 2.43l-4.727 4.645a.532.532 0 0 1-.743 0 .51.51 0 0 1 0-.73L26.318 1.7a.532.532 0 0 1 .743 0 .51.51 0 0 1 0 .73zm-8.349-1.09l-.869 2.792a.527.527 0 0 1-.655.343.515.515 0 0 1-.35-.645l.87-2.792a.527.527 0 0 1 .656-.342.515.515 0 0 1 .348.644zm9.023 9.55l-2.842.854a.527.527 0 0 1-.656-.343.515.515 0 0 1 .349-.644l2.842-.854a.527.527 0 0 1 .656.343.515.515 0 0 1-.349.644zM22.95.953l-2.678 4.38a.53.53 0 0 1-.721.176.511.511 0 0 1-.18-.708L22.05.42a.53.53 0 0 1 .721-.176c.249.146.33.464.18.708zm5.412 5.67l-4.458 2.632a.53.53 0 0 1-.72-.177.511.511 0 0 1 .18-.708L27.82 5.74a.53.53 0 0 1 .72.176.511.511 0 0 1-.179.708zm-14.28 19.7C7.488 26.323 2 20.93 2 14.452 2 7.793 7.14 2.58 14.082 2.58h.776l-.288.707-6.304 15.484-.116-.556 2.101 2.064-.566-.114 4.251-1.67.367-.145.243.306 2.549 3.22-.703-.115 1.605-1.032-.183.66-1.605-3.189-.252-.5.528-.208 1.4-.55 3.08-1.21 3.079-1.21 1.015-.4.284-.111.1-.04.721-.283v.763c0 7.163-5.11 11.87-12.082 11.87zm-5.295 1.564a9.025 9.025 0 0 1-.138-.047.514.514 0 0 1-.314-.662c.1-.267.4-.406.674-.308a22.7 22.7 0 0 0 1.692.501c.804.204 1.591.357 2.324.437.374.042.728.063 1.057.063.33 0 .683-.021 1.058-.063.732-.08 1.52-.233 2.324-.437a22.7 22.7 0 0 0 1.692-.501.528.528 0 0 1 .673.308.514.514 0 0 1-.314.662l-.138.047a23.759 23.759 0 0 1-1.65.483c-.85.216-1.684.378-2.47.464-.412.046-.805.07-1.175.07s-.763-.024-1.174-.07a18.303 18.303 0 0 1-2.47-.463 23.759 23.759 0 0 1-1.65-.484zm1.759 3.143a6.327 6.327 0 0 1-.307-.089.514.514 0 0 1-.332-.653.527.527 0 0 1 .664-.326 11.222 11.222 0 0 0 .988.242 13.14 13.14 0 0 0 2.523.248 13.14 13.14 0 0 0 3.26-.418c.126-.033.21-.059.251-.072a.527.527 0 0 1 .665.326.514.514 0 0 1-.333.653c-.058.02-.162.05-.307.09a14.2 14.2 0 0 1-3.536.454 14.2 14.2 0 0 1-3.536-.455z\" fill-rule=\"nonzero\"\u003e\u003c/path\u003e\u003c/svg\u003e Sander\u0027s Events \u003c/h2\u003e \u003cdl class=\"sz-group\"\u003e \u003cdt class=\"sz-group__title\"\u003eJune 2022\u003c/dt\u003e \u003cdd class=\"sz-item\"\u003e \u003ca href=\"https://tweakers.net/partners/devsummit2022/\" class=\"sz-item__title\" target=\"_blank\"\u003eTweakers Developers Summit 2022 \u003c/a\u003e \u003cspan class=\"sz-item__meta\"\u003eUtrecht, Netherlands\u003c/span\u003e \u003c/dd\u003e \u003cdt class=\"sz-group__title\"\u003eMay 2022\u003c/dt\u003e \u003cdd class=\"sz-item\"\u003e \u003ca href=\"https://ddog.nl/article/20220508-ddog10/\" class=\"sz-item__title\" target=\"_blank\"\u003eDDOG 10th edition\u003c/a\u003e \u003cspan class=\"sz-item__meta\"\u003e\u003c/span\u003e \u003c/dd\u003e \u003cdt class=\"sz-group__title\"\u003eNovember 2021\u003c/dt\u003e \u003cdd class=\"sz-item\"\u003e \u003ca href=\"https://www.meetup.com/sogeti-frontend/events/280834233/\" class=\"sz-item__title\" target=\"_blank\"\u003eSogeti Frontend Lightning Talks - Frontend Frameworks\u003c/a\u003e \u003cspan class=\"sz-item__meta\"\u003e\u003c/span\u003e \u003c/dd\u003e \u003c/dl\u003e \u003cdiv class=\"sz-powered-by\"\u003e \u003ca href=\"https://sessionize.com/\" target=\"_blank\"\u003epowered by \u003cstrong\u003eSessionize.com\u003c/strong\u003e\u003c/a\u003e \u003c/div\u003e \u003c/div\u003e \u003c/div\u003e');";
    var UnescapedHtml = Uri.UnescapeDataString(rawHtml);
    var sanitizedHtml = UnescapedHtml.Replace("document.write('", "").Replace("');", "");

    var doc = new HtmlDocument();
    doc.LoadHtml(sanitizedHtml);
    var rawEvents = doc.DocumentNode.SelectNodes("//ul/li"); // TODO: Use dd/dt, etc..

    var events = new List<Event>();

    return events;
}

record Session(string Title, Uri SessionzeUri);
record Event(string Title, string Location, Uri EventUri, DateOnly? Date);