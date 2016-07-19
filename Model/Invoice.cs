using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using IPar = iTextSharp.text.Paragraph;

//using AcroPDFLib;

namespace RESHOTEL_APP.Model
{
    class Invoice
    {
        #region properties
        private Meal ml = new Meal();
        private Booking bk = new Booking();
        private Room rm = new Room();
        private Promotion pr = new Promotion();

        private string contact { get; set; }
        private string staffFL { get; set; }
        private DateTime bookDate { get; set; }
        private string roomType { get; set; }
        private string paymentType { get; set; }
        private decimal dinnersRate { get; set; }
        private decimal breakfastsRate { get; set; }
        private int nbDinner { get; set; }
        private int nbBreakfast { get; set; }
        private DateTime checkin { get; set; }
        private DateTime checkout { get; set; }
        private int nbNights { get; set; }
        private int custId { get; set; }
        private int roomId { get; set; }
        private decimal nightRate { get; set; }
        private int resaNb { get; set; }

        private static int TVA = 10;
        private decimal dinnerPrice { get; set; }
        private decimal breakfastPrice { get; set; }
        private decimal roomPrice { get; set; }
        private decimal totalHT { get; set; }
        private decimal totalTTC { get; set; }
        private decimal roomTva { get; set; }
        private decimal breakfastTva { get; set; }
        private decimal dinnerTva { get; set; }
        private decimal optRate { get; set; }
        public int discount { get; set; }
        private decimal totalTVA { get; set; }
        #endregion

        /// <summary>
        /// Method to generate invoice booking in pdf
        /// </summary>
        /// <param name="_bookId">int</param>
        public void pdfGen(int _bookId)
        {
            this.getInvoiceInfo(_bookId);
            this.calculateTotalPrices();

            Document doc = new Document(iTextSharp.text.PageSize.A4);
            PdfWriter wri = PdfWriter.GetInstance(doc, new FileStream("Invoice.pdf", FileMode.Create));

            // First, create our fonts
            var titleFont = FontFactory.GetFont("Arial", 14, Font.BOLD);
            var TableHeaderFont = FontFactory.GetFont("Arial", 10, Font.BOLD);
            var bodyFont = FontFactory.GetFont("Arial", 10, Font.NORMAL);

            doc.Open();

            // Write stuff
            doc.Add(new IPar("Hôtel ÉFONN**", titleFont));
            doc.Add(new IPar("18 rue Magellan – 31670 LABEGE\nFrance", bodyFont));

            IPar pInv = new IPar("Facture N°. " + _bookId.ToString(), titleFont);
            pInv.Alignment = Element.ALIGN_RIGHT;
            doc.Add(pInv);

            doc.Add(new IPar("\n"));
            doc.Add(new IPar("\n"));
            doc.Add(new IPar("\n"));
            doc.Add(new IPar("\n"));

            #region Invoice information table
            PdfPTable tableInfo = new PdfPTable(2);
            PdfPCell cellInfo = new PdfPCell();

            tableInfo.HorizontalAlignment = 0;
            tableInfo.WidthPercentage = 40;
            tableInfo.SetWidths(new float[] { 4, 3 });  // then set the column's __relative__ widths
            tableInfo.DefaultCell.Border = Rectangle.NO_BORDER;
            tableInfo.DefaultCell.BackgroundColor = new BaseColor(System.Drawing.ColorTranslator.FromHtml("#ecf0f1"));
            //tableInfo.DefaultCell.Phrase = new Phrase() { Font = TableFont };

            cellInfo.Colspan = 2;
            cellInfo.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            cellInfo.Border = Rectangle.NO_BORDER;

            tableInfo.AddCell(cellInfo);
            tableInfo.AddCell(new Phrase("Date de la facture", bodyFont));
            tableInfo.AddCell(new Phrase(DateTime.Now.Date.ToString("dd/MM/yyyy"), bodyFont));
            tableInfo.AddCell(new Phrase("Référence de la facture", bodyFont));
            tableInfo.AddCell(new Phrase(_bookId.ToString(), bodyFont));
            tableInfo.AddCell(new Phrase("Numéro de client", bodyFont));
            tableInfo.AddCell(new Phrase(custId.ToString(), bodyFont));
            tableInfo.AddCell(new Phrase("Type de paiement", bodyFont));
            tableInfo.AddCell(new Phrase(paymentType, bodyFont));
            //tableInfo.AddCell(new Phrase("Modalité de paiement", bodyFont));
            //tableInfo.AddCell(new Phrase("30 jours", bodyFont));
            tableInfo.AddCell(new Phrase("Emis par", bodyFont));
            tableInfo.AddCell(new Phrase(Login.firstname + " " + Login.lastname, bodyFont));
            tableInfo.AddCell(new Phrase("Contact client", bodyFont));
            tableInfo.AddCell(new Phrase(contact, bodyFont));
            tableInfo.AddCell(new Phrase("Date de réservation", bodyFont));
            tableInfo.AddCell(new Phrase(bookDate.ToString("dd/MM/yyyy"), bodyFont));
            doc.Add(tableInfo);
            #endregion

            doc.Add(new IPar("\n"));
            doc.Add(new IPar("\n"));

            #region rate information price
            PdfPTable table = new PdfPTable(7);
            PdfPCell cell = new PdfPCell();

            table.WidthPercentage = 100;

            cell.Colspan = 7;
            cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            table.AddCell(cell);

            table.AddCell(new Phrase("Description", TableHeaderFont));
            table.AddCell(new Phrase("Quantités", TableHeaderFont));
            table.AddCell(new Phrase("Unités", TableHeaderFont));
            table.AddCell(new Phrase("Prix unitaires HT", TableHeaderFont));
            table.AddCell(new Phrase("% TVA", TableHeaderFont));
            table.AddCell(new Phrase("TVA", TableHeaderFont));
            table.AddCell(new Phrase("TOTAL TTC", TableHeaderFont));

            table.AddCell(new Phrase("Chambre "+ roomType, bodyFont));
            table.AddCell(new Phrase(nbNights.ToString(), bodyFont));
            table.AddCell(new Phrase("Nuitées", bodyFont));
            table.AddCell(new Phrase(nightRate.ToString() + " €", bodyFont));
            table.AddCell(new Phrase(TVA.ToString() + "%", bodyFont));
            table.AddCell(new Phrase(roomTva.ToString() + " €", bodyFont));
            table.AddCell(new Phrase(roomPrice.ToString() + " €", bodyFont));

            if (nbBreakfast != 0)
            {
                table.AddCell(new Phrase("Petit déjeuner", bodyFont));
                table.AddCell(new Phrase(nbBreakfast.ToString(), bodyFont));
                table.AddCell(new Phrase("nb.", bodyFont));
                table.AddCell(new Phrase(breakfastsRate.ToString() + " €", bodyFont));
                table.AddCell(new Phrase(TVA.ToString() + "%", bodyFont));
                table.AddCell(new Phrase(breakfastTva.ToString() + " €", bodyFont));
                table.AddCell(new Phrase(breakfastPrice.ToString() + " €", bodyFont));
            }

            if (nbDinner != 0)
            {
                table.AddCell(new Phrase("Dîner", bodyFont));
                table.AddCell(new Phrase(nbDinner.ToString(), bodyFont));
                table.AddCell(new Phrase("nb.", bodyFont));
                table.AddCell(new Phrase(dinnersRate.ToString() + " €", bodyFont));
                table.AddCell(new Phrase(TVA.ToString() + "%", bodyFont));
                table.AddCell(new Phrase(dinnerTva.ToString() + " €", bodyFont));
                table.AddCell(new Phrase(dinnerPrice.ToString() + " €", bodyFont));
            }

            foreach (var item in bk.getBookOptionInfo(_bookId))
            {
                table.AddCell(new Phrase(item.optionLabel, bodyFont));
                table.AddCell(new Phrase("", bodyFont));
                table.AddCell(new Phrase("", bodyFont));
                table.AddCell(new Phrase(item.optionRate.ToString() + " €", bodyFont));
                table.AddCell(new Phrase(TVA.ToString() + "%", bodyFont));
                table.AddCell(new Phrase((item.optionRate*TVA/100).ToString() + " €", bodyFont));
                table.AddCell(new Phrase(((item.optionRate * TVA / 100) + item.optionRate) + " €".ToString(), bodyFont));
            }

            doc.Add(table);
            #endregion

            doc.Add(new IPar("\n"));

            #region table total prices
            PdfPTable tableTotal = new PdfPTable(2);
            PdfPCell cellTotal = new PdfPCell();

            tableTotal.HorizontalAlignment = 0;
            tableTotal.WidthPercentage = 40;
            tableTotal.SetWidths(new float[] { 4, 3 });  // then set the column's __relative__ widths
            tableTotal.DefaultCell.Border = Rectangle.NO_BORDER;
            tableTotal.DefaultCell.BackgroundColor = new BaseColor(System.Drawing.ColorTranslator.FromHtml("#ecf0f1"));
            tableTotal.HorizontalAlignment = Element.ALIGN_RIGHT;
            //tableInfo.DefaultCell.Phrase = new Phrase() { Font = TableFont };

            cellTotal.Colspan = 2;
            cellTotal.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            cellTotal.Border = Rectangle.NO_BORDER;

            tableTotal.AddCell(cellTotal);
            tableTotal.AddCell(new Phrase("Total HT", TableHeaderFont));
            tableTotal.AddCell(new Phrase(totalHT.ToString() + " €", bodyFont));


            tableTotal.AddCell(new Phrase("Total TVA", TableHeaderFont));
            tableTotal.AddCell(new Phrase(totalTVA.ToString() + " €", bodyFont));

            if (resaNb > 1)
            {
                tableTotal.AddCell(new Phrase("Remise fidélité", TableHeaderFont));
                tableTotal.AddCell(new Phrase("-5%", bodyFont));
            }

            if (pr.getPromoByRoom(roomId, checkin, checkout).Count() > 0)
            {
                tableTotal.AddCell(new Phrase("Promotion", TableHeaderFont));
                tableTotal.AddCell(new Phrase( "-"+discount.ToString() + " %", bodyFont));
            }

            tableTotal.AddCell(new Phrase("Total TTC", TableHeaderFont));
            tableTotal.AddCell(new Phrase(totalTTC.ToString()+" €", bodyFont));

            doc.Add(tableTotal);
            #endregion

            doc.Add(new IPar("\n"));
            doc.Add(new IPar("\n"));
            //test commit
            doc.Close();
        }

        /// <summary>
        /// function to get all booking info for invoice
        /// </summary>
        /// <param name="_bookId">int</param>
        private void getInvoiceInfo(int _bookId)
        {
            
            foreach (var item in bk.getBookInfo(_bookId))
            {
                nbNights = (item.checkOut.Date - item.checkIn.Date).Days;
                nbBreakfast = item.nbBreakfast * nbNights;
                nbDinner = item.nbDinner * nbNights;
                contact = item.firstname + " " + item.lastname;
                custId = item.customerId;
                staffFL = item.staffFirst + " " + item.staffLast;
                bookDate = item.dateBooked;
                roomType = item.features;
                paymentType = item.paymentType;
                roomId = item.roomId;
                resaNb = item.nbResa;
                checkin = item.checkIn.Date;
                checkout = item.checkOut.Date;
            }

            foreach (var item in ml.getMealPrice())
            {
                dinnersRate = item.dinnerR;
                breakfastsRate = item.breakfastR;
            }

            foreach (var item in rm.getRoomInfo(roomId))
            {
                if (nbNights == 5)
                {
                    nightRate = item.price5n / nbNights;
                }
                else if (nbNights == 10)
                {
                    nightRate = item.price10n / nbNights;
                }
                else
                {
                    nightRate = item.price1n;
                }
            }

            foreach (var item in bk.getBookOptionInfo(_bookId))
            {
                optRate += item.optionRate;
            }

            if (pr.getPromoByRoom(roomId, checkin, checkout).Count() > 0)
            {
                foreach (var item in pr.getPromoByRoom(roomId, checkin, checkout))
                {
                    discount = (int)item.discount;
                }
            }

        }

        /// <summary>
        /// function to calculate all rates
        /// </summary>
        private void calculateTotalPrices()
        {
            roomTva = (nightRate * nbNights * TVA) / 100;
            roomPrice = (nightRate * nbNights) + roomTva;

            dinnerTva = (dinnersRate * nbNights * TVA) / 100;
            dinnerPrice = (dinnersRate * nbNights) + dinnerTva;

            breakfastTva = (breakfastsRate * nbNights * TVA) / 100;
            breakfastPrice = (breakfastsRate * nbNights) + breakfastTva;

            totalHT = (nightRate * nbNights) + (dinnersRate * nbNights) + (breakfastsRate * nbNights) + optRate;
            totalTVA = roomTva + dinnerTva + breakfastTva + (optRate * TVA / 100);
            totalTTC = roomPrice + dinnerPrice + breakfastPrice + ((optRate * TVA / 100) + optRate);

            if (resaNb > 1)
            {
                totalTTC = totalTTC - (totalTTC * 5 / 100);
            }

            if (pr.getPromoByRoom(roomId, checkin, checkout).Count() > 0)
            {
                totalTTC = totalTTC - (totalTTC * discount / 100);
            }
        }

        //AxAcroPDFLib.AxAcroPDF axAcroPDF1;

        //var file = Path.GetTempFileName();
        //string filepath = Path.GetTempPath();
        //string strFilename = Path.GetFileName(file);
        //using (MemoryStream ms = new MemoryStream())
        //{
        //    Document doc = new Document();
        //    //PdfWriter.GetInstance(doc, ms);
        //    PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(Path.Combine(filepath, strFilename), FileMode.Create));
        //    doc.AddTitle("Document Title");
        //    doc.Open();
        //    doc.Add(new Paragraph("My paragraph. Bla Bla Test"));
        //    doc.Close();
        //}
        //axAcroPDF1.src = Path.Combine(filepath, strFilename);



    }
}
