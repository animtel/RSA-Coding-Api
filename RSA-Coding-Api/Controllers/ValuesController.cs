using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace RSA_Coding_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly long d = 179;
        private readonly long n = 209;

        [HttpPost, Route("decodeMsg")]
        public ActionResult<string> decodeMessg([FromBody]string code)
        {
            string res = String.Empty;
            try
            {
                res = decode(code.Split(" ").AsEnumerable(), d, n);
            }
            catch (Exception ex)
            {
                res = ex.Message;
            }
            return res;
        }

        public Func<IEnumerable<string>, long, long, string> decode = (msgCollection, d, n) =>
            String.Concat(msgCollection
                .Select(msg =>
                characters[
                    Convert.ToInt32(
                        (BigInteger.Pow(
                            new BigInteger(
                                Convert.ToDouble(msg)
                                ), (int)d
                            ) % new BigInteger((int)n)).ToString()
                        )
                    ].ToString()
                )
                );

        private string RSA_Dedoce(List<string> input, long d, long n)
        {
            string result = "";

            BigInteger bi;

            foreach (string item in input)
            {
                bi = new BigInteger(Convert.ToDouble(item));
                bi = BigInteger.Pow(bi, (int)d);

                BigInteger n_ = new BigInteger((int)n);

                bi = bi % n_;

                int index = Convert.ToInt32(bi.ToString());

                result += characters[index].ToString();
            }

            return result;
        }

        static char[] characters = new char[] { '#', 'А', 'Б', 'В', 'Г', 'Д', 'Е', 'Ё', 'Ж', 'З', 'И',
                                                'Й', 'К', 'Л', 'М', 'Н', 'О', 'П', 'Р', 'С',
                                                'Т', 'У', 'Ф', 'Х', 'Ц', 'Ч', 'Ш', 'Щ', 'Ь', 'Ы', 'Ъ',
                                                'Э', 'Ю', 'Я', ' ', '1', '2', '3', '4', '5', '6', '7',
                                                '8', '9', '0' };
    }
}
