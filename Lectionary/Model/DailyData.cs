using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Lectionary.Model
{
    public class DailyData
    {
        public DateTime Day { get; set; }
        public string FeastsAndSaints { get; set; }
        public List<ReadingData> ReadingList { get; set; } = new List<ReadingData>();
        public LectionaryDate LectionaryEntry { get; set; }

        public DailyData(LectionaryDate lectionaryEntry, Bible bible)
        {
            LectionaryEntry = FilteredLectionaryDate(lectionaryEntry, bible);
            FeastsAndSaints = LectionaryEntry.FeastsAndSaints.Trim();
            FillReadingList(bible);
            Day = LectionaryEntry.Day;
        }

        private void FillReadingList(Bible bible)
        {
            foreach (Reading reading in LectionaryEntry.Readings)
            {
                ReadingData data = new ReadingData(reading, bible);
                ReadingList.Add(data);
            }
        }

        private LectionaryDate FilteredLectionaryDate(LectionaryDate lectionaryEntry, Bible bible)
        {
            LectionaryDate filteredLectionaryDate = new LectionaryDate();
            filteredLectionaryDate.Day = lectionaryEntry.Day;
            filteredLectionaryDate.FeastsAndSaints = lectionaryEntry.FeastsAndSaints.Trim() ;
            foreach(Reading reading in lectionaryEntry.Readings)
            {
                if (bible.books.Where(Book => Book.name == reading.Book).Any())
                {
                    if (reading.EndChapter != 0 && reading.EndVerse != 0)
                    {
                        filteredLectionaryDate.Readings.Add(reading);
                    }
                }
            }
            return filteredLectionaryDate;
        }
    }

    public class ReadingData
    {
        public List<VerseText> Verses { get; set; } = new List<VerseText>();

        public ReadingData(Reading reading, Bible bible)
        {
            FillVerses(reading, bible);
        }

        private void FillVerses(Reading reading, Bible bible)
        {
            for (int j = reading.StartChapter; j <= reading.EndChapter; j++)
            {

                int startVerse = (j > reading.StartChapter) ? 1 : reading.StartVerse;

                for (int i = startVerse; i <= reading.EndVerse; i++)
                {
                    try
                    {
                        var a = bible.books.Where(Book => Book.name == reading.Book).First();
                        var b = a.chapters.Where(Chapters => Chapters.num == j).First();
                        var c = b.verses.Where(Verse => Verse.num == i).First();
                        VerseText thisVerse = new VerseText(c.text, c.num, b.num);
                        Verses.Add(thisVerse);
                    }
                    catch { };
                }
            }
        }
    }

    public class VerseText
    {
        public string Text { get; set; }
        public int VerseNumber { get; set; }
        public int ChapterNumber { get; set; }

        public VerseText(string text, int verseNumber, int chapterNumber)
        {
            Text = text;
            VerseNumber = verseNumber;
            ChapterNumber = chapterNumber;
        }
    }
}
