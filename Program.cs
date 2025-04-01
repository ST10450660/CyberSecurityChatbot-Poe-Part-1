using System;
using System.IO;
using System.Media;
using System.Text.RegularExpressions;
using System.Collections;
using System.Linq;

class CybersecurityChatbot
{
    static void Main()
    {
        SetupConsole();      // Configure console appearance
        PlayVoiceGreeting(); // Play greeting sound
        DisplayAsciiImage(); // Show ASCII logo
        StartChatbot();      // Begin user interaction
    }

    // Configures the console for better visibility
    static void SetupConsole()
    {
        Console.Title = "Cybersecurity Awareness Chatbot";
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.BackgroundColor = ConsoleColor.Black;
        Console.Clear();
    }

    // Plays a voice greeting when the chatbot starts
    static void PlayVoiceGreeting()
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("\nWelcome to the Cybersecurity Awareness Chatbot!");
        Console.WriteLine("I’m here to help you learn about online safety in a fun and friendly way.\n");
        Console.ResetColor();

        string audioFilePath = "greeting.wav"; // Path to the greeting audio file

        if (File.Exists(audioFilePath))
        {
            try
            {
                using (SoundPlayer player = new SoundPlayer(audioFilePath))
                {
                    player.PlaySync(); // Play sound synchronously
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"[ERROR] Unable to play sound: {ex.Message}");
                Console.ResetColor();
            }
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("[ERROR] Voice greeting file not found.");
            Console.ResetColor();
        }
    }

    // Displays an ASCII image to welcome users
    static void DisplayAsciiImage()
    {
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine(@"
      _.-""      ""-._
    /              \
   |                |
   |,  .-.  .-.  ,|
   | )(_o/  \o_)( |
   |/     /\     \|
   (_     ^^     _)
    \__|IIIIII|__/
     | \IIIIII/ |
      \        /
       `------`
                    
");
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("\nWelcome to the Cybersecurity Awareness Chatbot!");
        Console.WriteLine("I’m here to help you stay safe online while making it fun.");
        Console.WriteLine("Ask me anything about cybersecurity or type 'exit' to quit.");
        Console.WriteLine("Let's make the web safer, one question at a time!");
        Console.ResetColor();
    }

    // Handles chatbot interaction
    static void StartChatbot()
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write("\nHey there! What's your name? ");
        string userName = GetValidatedInput(); // Get validated user input for name

        Console.WriteLine($"\nNice to meet you, {userName}! Let’s explore the world of cybersecurity together.");
        Console.ResetColor();

        // Create an ArrayList of cybersecurity topics and their answers
        var responses = new ArrayList
        {
            new string[] { "how are you?", "I'm just a bot, but I’m super excited to help you stay safe online!", "Just ask me about anything related to cybersecurity, and I’ll give you tips to stay safe!" },
            new string[] { "what is your purpose?", "I help educate users like you on how to stay safe in the digital world.", "Ask me about topics like passwords, phishing, and safe browsing!" },
            new string[] { "password", "A strong password is your first line of defense! Use at least 12 characters, mixing uppercase and lowercase letters, numbers, and symbols.", "To create a strong password: Use a mix of characters, avoid using common phrases, and never reuse passwords. Consider using a password manager to generate and store them safely." },
            new string[] { "phishing", "Phishing attacks trick you into giving up your personal information. Always check the sender’s email and avoid clicking on suspicious links!", "To avoid phishing: Never click on links from unknown senders, and always verify the URL. Look for signs like spelling errors or strange email addresses." },
            new string[] { "safe browsing", "To browse safely, look for HTTPS in the URL and avoid unknown links. Keep your browser and extensions updated to stay secure.", "To stay safe: Use trusted websites, keep your browser updated, and enable pop-up blockers." },
            new string[] { "two-factor authentication", "2FA adds an extra layer of security to your accounts. It’s like a double lock for your data!", "Enable 2FA on your accounts to add an extra layer of protection. It’s an easy way to keep your accounts safe, especially for sensitive ones like banking or email." },
            new string[] { "secure website", "A secure website has 'https://' and a padlock icon. If it’s missing, you might want to stay away from it!", "Before entering sensitive information, ensure the website is secure. Look for the HTTPS prefix and the padlock icon in the address bar." },
            new string[] { "malware", "Malware is malicious software that can harm your device or steal your data. Keep your antivirus updated and scan regularly!", "To avoid malware: Keep your antivirus up to date, don't open unknown email attachments, and download software only from trusted sources." },
            new string[] { "vpn", "A VPN encrypts your internet connection, keeping your data private and secure. It’s like wearing an invisibility cloak while browsing!", "Always use a VPN when browsing on public Wi-Fi networks to protect your privacy. It encrypts your data, making it harder for others to snoop." },
            new string[] { "firewall", "A firewall acts as a security barrier, keeping hackers out of your network. It's like a virtual gatekeeper!", "Enable your firewall on all devices to block unauthorized access and keep intruders at bay." },
            new string[] { "social engineering", "Cybercriminals use tricks to get your personal information. Always verify before sharing sensitive data!", "To protect yourself: Be skeptical of unsolicited requests for information, and always verify the identity of the person you're communicating with." },
            new string[] { "ransomware", "Ransomware locks your files and demands payment. Regular backups can help you avoid paying the ransom!", "To avoid ransomware: Regularly back up your important files, avoid clicking on suspicious links, and keep your software updated." },
            new string[] { "data breach", "A data breach happens when sensitive information is exposed. Protect your data with strong passwords and encryption.", "To minimize the impact of a data breach: Use strong, unique passwords for each account and enable encryption to protect sensitive information." },
            new string[] { "cyber hygiene", "Cyber hygiene means practicing good habits online, like updating your software and using strong passwords. It's like brushing your teeth, but for the internet!", "Good cyber hygiene includes regularly updating software, using strong passwords, backing up data, and being aware of phishing scams." },
            new string[] { "spyware", "Spyware secretly tracks your activity. Avoid downloading shady software and use trusted antivirus programs to keep it at bay.", "To avoid spyware: Don't download software from untrusted sources and always scan files with antivirus software before opening." },
            new string[] { "identity theft", "Identity theft occurs when someone steals your personal information. Stay vigilant by monitoring your accounts and using unique passwords.", "To protect yourself: Regularly monitor your bank accounts, use credit monitoring services, and create strong, unique passwords for every service." },
            new string[] { "backups", "Backups ensure you don’t lose your important data. Whether it's the cloud or an external hard drive, make sure you back up regularly!", "Always create backups of your important files and store them in multiple locations, like an external hard drive and the cloud." }
        };

        while (true) // Chatbot runs in a loop until the user exits
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("\nWhat would you like to know about cybersecurity? (Type 'exit' to quit) ");
            string userInput = Console.ReadLine()?.ToLower().Trim(); // Convert input to lowercase

            if (userInput == "exit")
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\nGoodbye, {userName}! Remember, online safety is important. Stay cyber-safe!");
                break; // Exit chatbot
            }
            else if (!string.IsNullOrWhiteSpace(userInput))
            {
                HandleUserQuery(userInput, responses); // Process user query
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Oops! I didn’t quite catch that. Can you ask me something about cybersecurity?");
            }

            // Ask if the user has more questions
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("\nDo you have any other questions about cybersecurity? (yes/no): ");
            string continueChat = Console.ReadLine()?.ToLower().Trim();

            if (continueChat == "no")
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\nGoodbye, {userName}! Stay safe online!");
                break; // Exit chatbot
            }
            else if (continueChat == "yes")
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\nGreat! Feel free to ask more questions about cybersecurity.");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("I didn’t quite understand that, but I’ll assume you still have questions. Let’s continue.");
            }

            Console.ResetColor();
        }
    }

    // Processes user questions and provides cybersecurity responses
    static void HandleUserQuery(string query, ArrayList responses)
    {
        string sanitizedQuery = query.ToLower().Trim();

        bool isValidQuery = false;

        foreach (string[] response in responses)
        {
            if (sanitizedQuery.Contains(response[0].ToLower())) // Check if query contains the keyword
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(response[1]); // Answer
                Console.WriteLine($"To stay safe: {response[2]}"); // Suggestions
                isValidQuery = true;
                break;
            }
        }

        if (!isValidQuery)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("I didn’t quite understand that. Could you ask in a different way? Here are some fun topics you can ask about:");
            foreach (string[] response in responses)
            {
                Console.WriteLine($"- {response[0]}");
            }
        }

        Console.ResetColor();
    }

    // Validates user input for name (only letters allowed)
    static string GetValidatedInput()
    {
        string input = Console.ReadLine();
        while (string.IsNullOrWhiteSpace(input) || !Regex.IsMatch(input, @"^[a-zA-Z]+$")) // Check if input contains only letters
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("Oops! Please enter a valid name (only letters allowed): ");
            Console.ResetColor();
            input = Console.ReadLine();
        }
        return input;
    }
}
